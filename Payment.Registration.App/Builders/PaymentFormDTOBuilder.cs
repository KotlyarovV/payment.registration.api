using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Payment.Registration.App.DTOs;
using Payment.Registration.Domain.Models;
using Type = System.Type;

namespace Payment.Registration.App.Builders
{
    public class ApplicantBuilder : AbstractBuilder<ApplicantSaveDto, Applicant>,
        IBuilder<ApplicantSaveDto, Applicant>
    {
        protected override void CreateMapping(IMappingExpression<ApplicantSaveDto, Applicant> cfg)
        {
            cfg
                .ForMember(a => a.Id,
                    expression => expression.MapFrom(a => Guid.NewGuid()));
        }
    }

    public class FileBuilder : AbstractBuilder<FileSaveDto, File>,
        IBuilder<FileSaveDto, File>
    {
        protected override void CreateMapping(IMappingExpression<FileSaveDto, File> cfg)
        {
            cfg.ForMember(f => f.Id,
                    expression => expression.MapFrom(f => Guid.NewGuid()))
                .ForMember(f => f.WayToFile,
                    expression =>
                        expression.MapFrom(
                            f => $"{Guid.NewGuid()}{Guid.NewGuid()}"));
        }
    }

    public class PaymentPositionBuilder : AbstractBuilder<PaymentPositionSaveDto, PaymentPosition>,
        IBuilder<PaymentPositionSaveDto, PaymentPosition, IReadOnlyCollection<File>>
    {
        protected override void CreateMapping(IMappingExpression<PaymentPositionSaveDto, PaymentPosition> cfg)
        {
            cfg.ForMember(p => p.Files, 
                expression => expression.MapFrom(p => new List<File>()))
        }

        public IReadOnlyCollection<File> Build(PaymentPositionSaveDto source1, PaymentPosition source)
        {
            throw new NotImplementedException();
        }
    }
    
    public class PaymentFormBuilder : AbstractBuilder<PaymentFormSaveDto, PaymentForm>,
        IBuilder<PaymentFormSaveDto, int, PaymentForm>
    {
        private readonly IBuilder<ApplicantSaveDto, Applicant> applicantBuilder;

        public PaymentFormBuilder(IBuilder<ApplicantSaveDto, Applicant> applicantBuilder)
        {
            this.applicantBuilder = applicantBuilder;
        }
        
        protected override void CreateMapping(IMappingExpression<PaymentFormSaveDto, PaymentForm> cfg)
        {
            cfg.ForMember(p => p.Id, 
                expression => expression.MapFrom(p => Guid.NewGuid()))
                .ForMember(p => p.Date,
                    expression => expression.MapFrom(p => DateTime.UtcNow))
                .ForMember(p => p.Type,
                    expression => 
                        expression.MapFrom(p => (Domain.Models.Type) p.Type))
                .ForMember(p => p.Number, expression => expression.Ignore())
                .ForMember(p => p.Applicant,
                    expression => expression.MapFrom(p => applicantBuilder.Build(p.Applicant)))
                .ForMember(p => p.Items,
                    expression => expression.MapFrom(p => p.Items.Select()))
        }

        public PaymentForm Build(PaymentFormSaveDto source1, int source)
        {
            throw new NotImplementedException();
        }
    }

    public class PaymentFormDTOBuilder : AbstractBuilder<PaymentForm, PaymentFormDto>,
        IBuilder<PaymentForm, PaymentFormDto>
    {
        private readonly IBuilder<Applicant, ApplicantDto> applicantBuilder;
        private readonly IBuilder<PaymentPosition, PaymentPositionDto> paymentPositionDtoBuilder;

        public PaymentFormDTOBuilder(IBuilder<Applicant, ApplicantDto> applicantBuilder,
            IBuilder<PaymentPosition, PaymentPositionDto> paymentPositionDtoBuilder)
        {
            this.applicantBuilder = applicantBuilder;
            this.paymentPositionDtoBuilder = paymentPositionDtoBuilder;
        }
        
        protected override void CreateMapping(IMappingExpression<PaymentForm, PaymentFormDto> cfg)
        {
            cfg
                .ForMember(pfDto => pfDto.Type,
                    expression => expression
                        .MapFrom(pf => (TypeDto) pf.Type))
                .ForMember(pfDto => pfDto.Applicant,
                    expression => expression
                        .MapFrom(pf => applicantBuilder.Build(pf.Applicant)))
                .ForMember(pfDto => pfDto.Items, 
                    expression => expression
                        .MapFrom(pf => pf.Items.Select(p => paymentPositionDtoBuilder.Build(p)).ToArray()));
        }
    }
}