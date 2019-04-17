using System;
using System.Collections.Generic;
using AutoMapper;
using Payment.Registration.App.DTOs;
using Payment.Registration.Domain.Models;
using Type = System.Type;

namespace Payment.Registration.App.Builders
{
    public class PaymentFormBuilder : AbstractBuilder<PaymentFormSaveDto, PaymentForm>,
        IBuilder<PaymentFormSaveDto, IEnumerable<PaymentPosition>, int, PaymentForm>
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
                    expression => expression.MapFrom(p => new List<PaymentPosition>()));
        }

        public PaymentForm Build(PaymentFormSaveDto source, IEnumerable<PaymentPosition> source2, int source3)
        {
            var paymentForm = Build(source);
            paymentForm.Number = source3;

            foreach (var paymentPosition in source2)
            {
                paymentForm.Items.Add(paymentPosition);
            }

            return paymentForm;
        }
    }
}