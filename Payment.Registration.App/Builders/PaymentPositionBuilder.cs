using System.Collections.Generic;
using AutoMapper;
using Payment.Registration.App.DTOs;
using Payment.Registration.Domain.Models;

namespace Payment.Registration.App.Builders
{
    public class PaymentPositionBuilder : AbstractBuilder<PaymentPositionSaveDto, PaymentPosition>,
        IBuilder<PaymentPositionSaveDto, IEnumerable<File>, PaymentPosition>
    {
        protected override void CreateMapping(IMappingExpression<PaymentPositionSaveDto, PaymentPosition> cfg)
        {
            cfg.ForMember(p => p.Files,
                expression => expression.MapFrom(p => new List<File>()));
        }

        public PaymentPosition Build(PaymentPositionSaveDto source, IEnumerable<File> files)
        {
            var destination = Build(source);

            foreach (var file in files)
            {
                destination.Files.Add(file);
            }

            return destination;
        }
    }
}