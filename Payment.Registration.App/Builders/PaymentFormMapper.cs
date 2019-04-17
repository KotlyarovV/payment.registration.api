using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Payment.Registration.App.DTOs;
using Payment.Registration.Domain.Models;

namespace Payment.Registration.App.Builders
{
    public class PaymentFormMapper : AbstractBuilder<PaymentFormUpdateDto, PaymentForm> , 
        IMapper<PaymentFormUpdateDto, IReadOnlyCollection<PaymentPosition>, PaymentForm>
    {
        private readonly IBuilder<ApplicantSaveDto, Applicant> applicantUpdateBuilder;

        public PaymentFormMapper(IBuilder<ApplicantSaveDto, Applicant> applicantUpdateBuilder)
        {
            this.applicantUpdateBuilder = applicantUpdateBuilder;
        }
        
        protected override void CreateMapping(IMappingExpression<PaymentFormUpdateDto, PaymentForm> cfg)
        {
            cfg
                .ForMember(p => p.Id, expression => expression.UseDestinationValue())
                .ForMember(p => p.Date, expression => expression.UseDestinationValue())
                .ForMember(p => p.Number, expression => expression.UseDestinationValue())
                .ForMember(p => p.Items, expression => expression.Ignore())
                .ForMember(p => p.Type, 
                    expression => expression.MapFrom(p => (Domain.Models.Type)p.Type))
                .ForMember(p => p.Applicant, expression => expression.Ignore());
        }

        public PaymentForm Map(PaymentFormUpdateDto paymentDto, IReadOnlyCollection<PaymentPosition> positions, PaymentForm destination)
        {
            Map(paymentDto, destination);

            applicantUpdateBuilder.Map(paymentDto.Applicant, destination.Applicant);

            var newPositions = new HashSet<Guid>(positions.Select(p => p.Id));
            var oldPositions = new HashSet<Guid>(destination.Items.Select(p => p.Id));

            foreach (var item in destination.Items.Union(positions).ToList())
            {
                if (!newPositions.Contains(item.Id))
                {
                    destination.Items.Remove(item);
                }
                else if (newPositions.Contains(item.Id) && !oldPositions.Contains(item.Id))
                {
                    destination.Items.Add(item);
                }
            }
            
            return destination;
        }
    }
}