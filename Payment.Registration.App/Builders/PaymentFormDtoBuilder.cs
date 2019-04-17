using System.Linq;
using AutoMapper;
using Payment.Registration.App.DTOs;
using Payment.Registration.Domain.Models;
using Type = System.Type;

namespace Payment.Registration.App.Builders
{
    public class PaymentFormDtoBuilder : AbstractBuilder<PaymentForm, PaymentFormDto>,
        IBuilder<PaymentForm, PaymentFormDto>
    {
        private readonly IBuilder<Applicant, ApplicantDto> applicantBuilder;
        private readonly IBuilder<PaymentPosition, PaymentPositionDto> paymentPositionDtoBuilder;

        public PaymentFormDtoBuilder(IBuilder<Applicant, ApplicantDto> applicantBuilder,
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