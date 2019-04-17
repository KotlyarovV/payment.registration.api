using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Payment.Registration.App.Builders;
using Payment.Registration.App.DTOs;
using Payment.Registration.Domain.Models;
using Payment.Registration.Domain.Specifications;
using File = Payment.Registration.Domain.Models.File;

namespace Payment.Registration.App.Services
{
    public class PaymentFormAppService
    {
        private readonly IPaymentFormDataService paymentFormDataService;
        private readonly IBuilder<PaymentForm, PaymentFormDto> paymentFormDtoBuilder;
        private readonly IBuilder<FileSaveDto, File> fileBuilder;
        private readonly IBuilder<PaymentPositionSaveDto, IEnumerable<File>, PaymentPosition> paymentPositionBuilder;
        private readonly IBuilder<PaymentFormSaveDto, IEnumerable<PaymentPosition>, int, PaymentForm> paymentFormBuilder;
        private readonly IFileStorageService fileStorageService;
        private readonly IPaymentPositionUpdateBuilder paymentPositionUpdateBuilder;
        private readonly IMapper<PaymentFormUpdateDto, IReadOnlyCollection<PaymentPosition>, PaymentForm> paymentFormUpdateBuilder;

        public PaymentFormAppService(IPaymentFormDataService paymentFormDataService,
            IBuilder<PaymentForm, PaymentFormDto> paymentFormDtoBuilder,
            IBuilder<FileSaveDto, File> fileBuilder,
            IBuilder<PaymentPositionSaveDto, IEnumerable<File>, PaymentPosition> paymentPositionBuilder,
            IBuilder<PaymentFormSaveDto, IEnumerable<PaymentPosition>, int, PaymentForm> paymentFormBuilder,
            IFileStorageService fileStorageService,
            IPaymentPositionUpdateBuilder paymentPositionUpdateBuilder,
            IMapper<PaymentFormUpdateDto, IReadOnlyCollection<PaymentPosition>, PaymentForm> paymentFormUpdateBuilder)
        {
            this.paymentFormDataService = paymentFormDataService;
            this.paymentFormDtoBuilder = paymentFormDtoBuilder;
            this.fileBuilder = fileBuilder;
            this.paymentPositionBuilder = paymentPositionBuilder;
            this.paymentFormBuilder = paymentFormBuilder;
            this.fileStorageService = fileStorageService;
            this.paymentPositionUpdateBuilder = paymentPositionUpdateBuilder;
            this.paymentFormUpdateBuilder = paymentFormUpdateBuilder;
        }

        public async Task<IEnumerable<PaymentFormDto>> Get()
        {
            var paymentForms = await paymentFormDataService.GetAll();
            var paymentFormsDto = paymentForms
                .Select(paymentFormDtoBuilder.Build)
                .ToArray();

            return paymentFormsDto;
        }

        public async Task Update(Guid paymentFormId, PaymentFormUpdateDto paymentFormUpdateDto)
        {
            var spec = new PaymentFormIdSpecification(paymentFormId);
            var paymentForm = await paymentFormDataService.Get(spec);
            var existedFiles = paymentForm.Items
                .SelectMany(i => i.Files)
                .ToDictionary(f => f.Id);
            var files = paymentFormUpdateDto.Items
                .SelectMany(i => i.Files, (pfDto, f) => new {Item = pfDto, FileDto = f})
                .Select(f => new
                {
                    f.Item, f.FileDto, File = f.FileDto.Id.HasValue 
                        ? existedFiles[f.FileDto.Id.Value] 
                        : fileBuilder.Build(f.FileDto.File)
                })
                .ToArray();

            var newFilesToSave = files.Where(f => !f.FileDto.Id.HasValue)
                .Select(f => new
                {
                    Bytes = Convert.FromBase64String(f.FileDto.File.FileInBase64), f.File.WayToFile
                });
            
            await Task.WhenAll(newFilesToSave.Select(f => fileStorageService.Save(
                new MemoryStream(f.Bytes), f.WayToFile)));
            
            var existedItems = paymentForm.Items.ToDictionary(i => i.Id);
            var items = paymentFormUpdateDto.Items
                .GroupJoin(files, p => p, p => p.Item, (dto, fs) =>  dto.Id.HasValue
                        ? paymentPositionUpdateBuilder.Map(dto, fs.Select(f => f.File).ToArray(),
                            existedItems[dto.Id.Value])
                        : paymentPositionUpdateBuilder.Build(dto, fs.Select(f => f.File).ToArray()))
                .ToArray();

            paymentFormUpdateBuilder.Map(paymentFormUpdateDto, items, paymentForm);
            await paymentFormDataService.Update(paymentForm);
        }
        
        public async Task<Guid> Add(PaymentFormSaveDto paymentFormSaveDto)
        {
            var files = paymentFormSaveDto.Items
                .SelectMany(i => i.Files,
                    (p, f) => new {Item = p, FileDTO = f, File = fileBuilder.Build(f)})
                .ToArray();

            await Task.WhenAll(files.Select(f => fileStorageService.Save(
                new MemoryStream(Convert.FromBase64String(f.FileDTO.FileInBase64)),
                f.File.WayToFile)));

            var items = paymentFormSaveDto.Items
                .GroupJoin(files, p => p, p => p.Item,
                    (p, pp) => paymentPositionBuilder.Build(p, pp.Select(f => f.File).ToArray()))
                .ToArray();
            
            var number = await paymentFormDataService.GetNewPaymentFormNumber();
            var paymentForm = paymentFormBuilder.Build(paymentFormSaveDto, items, number);
            var paymentFormSaved = await paymentFormDataService.Add(paymentForm);

            return paymentFormSaved.Id;
        }

        public async Task Delete(Guid id)
        {
            var spec = new PaymentFormIdSpecification(id);
            var paymentForm = await paymentFormDataService.Get(spec);
            var number = paymentForm.Number;

            foreach (var file in paymentForm.Items.SelectMany(i => i.Files))
            {
                await fileStorageService.Delete(file.WayToFile);
            }
            
            await paymentFormDataService.Remove(paymentForm);
            
            var numberSpec = new PaymentFormNumbersMoreThenSpecification(number);
            var paymentForms = await paymentFormDataService.GetAll(numberSpec);
            
            foreach (var form in paymentForms)
            {
                form.Number -= 1;
                await paymentFormDataService.Update(form);
            }
        }
    }
}