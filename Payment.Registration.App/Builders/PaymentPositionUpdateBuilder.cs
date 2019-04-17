using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Payment.Registration.App.DTOs;
using Payment.Registration.Domain.Models;

namespace Payment.Registration.App.Builders
{
    public class PaymentPositionUpdateBuilder : AbstractBuilder<PaymentPositionUpdateDto, PaymentPosition>,
        IPaymentPositionUpdateBuilder
    {
        protected override void CreateMapping(IMappingExpression<PaymentPositionUpdateDto, PaymentPosition> cfg)
        {
            cfg
                .ForMember(p => p.Id, expression => expression.UseDestinationValue())
                .ForMember(p => p.Sum,
                    expression => expression.MapFrom(pu => pu.Sum))
                .ForMember(p => p.Comment,
                    expression => expression.MapFrom(pu => pu.Comment))
                .ForMember(p => p.SortOrder,
                    expression => expression.MapFrom(pu => pu.SortOrder))
                .ForMember(p => p.Files, 
                    expression => expression.Ignore());
        }

        public PaymentPosition Build(PaymentPositionUpdateDto source, IReadOnlyCollection<File> files)
        {
            var result = Build(source);
            result.Id = Guid.NewGuid();
            result.Files = files.ToList();
            return result;
        }

        public PaymentPosition Map(PaymentPositionUpdateDto source, IReadOnlyCollection<File> files, PaymentPosition destination)
        {
            Map(source, destination);
            
            var existedFileIds = new HashSet<Guid>(destination.Files.Select(f => f.Id));
            var newFileIds = new HashSet<Guid>(files.Select(f => f.Id));

            foreach (var file in destination.Files.Union(files).ToList())
            {
                if (!newFileIds.Contains(file.Id))
                {
                    destination.Files.Remove(file);
                }
                else if (newFileIds.Contains(file.Id) && !existedFileIds.Contains(file.Id))
                {
                    destination.Files.Add(file);
                }
            }
            
            return destination;
        }
    }
}