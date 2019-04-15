using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Payment.Registration.App.Builders;
using Payment.Registration.App.DTOs;
using Payment.Registration.Domain.Models;
using File = Payment.Registration.Domain.Models.File;

namespace Payment.Registration.App.Services
{
    public interface IFileStorageService
    {
        Task Save(MemoryStream memoryStream, string wayToFile);
    }
    
    public interface IDataService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> Add(TEntity entity);
    }
    
    public class PaymentFormAppService
    {
        private readonly IDataService<PaymentForm> paymentFormDataService;
        private readonly IBuilder<PaymentForm, PaymentFormDto> paymentFormDtoBuilder;
        private readonly IBuilder<FileSaveDto, File> fileBuilder;
        private readonly IBuilder<PaymentPositionSaveDto, IEnumerable<File>, PaymentPosition> paymentPositionBuilder;
        private readonly IBuilder<PaymentFormSaveDto, IEnumerable<PaymentPosition>, PaymentForm> paymentFormBuilder;
        private readonly IFileStorageService fileStorageService;

        public PaymentFormAppService(IDataService<PaymentForm> paymentFormDataService,
            IBuilder<PaymentForm, PaymentFormDto> paymentFormDtoBuilder,
            IBuilder<FileSaveDto, File> fileBuilder,
            IBuilder<PaymentPositionSaveDto, IEnumerable<File>, PaymentPosition> paymentPositionBuilder,
            IBuilder<PaymentFormSaveDto, IEnumerable<PaymentPosition>, PaymentForm> paymentFormBuilder,
            IFileStorageService fileStorageService)
        {
            this.paymentFormDataService = paymentFormDataService;
            this.paymentFormDtoBuilder = paymentFormDtoBuilder;
            this.fileBuilder = fileBuilder;
            this.paymentPositionBuilder = paymentPositionBuilder;
            this.paymentFormBuilder = paymentFormBuilder;
            this.fileStorageService = fileStorageService;
        }

        public async Task<IEnumerable<PaymentFormDto>> Get()
        {
            var paymentForms = await paymentFormDataService.GetAll();
            var paymentFormsDto = paymentForms
                .Select(paymentFormDtoBuilder.Build)
                .ToArray();

            return paymentFormsDto;
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

            var items = files
                .GroupBy(f => f.Item,
                    (i, fs) => paymentPositionBuilder.Build(i, fs.Select(f => f.File).ToArray()))
                .ToArray();

            var paymentForm = paymentFormBuilder.Build(paymentFormSaveDto, items);
            var paymentFormSaved = await paymentFormDataService.Add(paymentForm);

            return paymentFormSaved.Id;
            /*foreach (var f in files)
            {
                await fileStorageService.Save(new MemoryStream(Convert.FromBase64String(f.FileDTO.FileInBase64)),
                    f.File.WayToFile);
            }*/
        }
    }
}