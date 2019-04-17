using System.Linq;
using AutoMapper;
using Payment.Registration.App.DTOs;
using Payment.Registration.Domain.Models;

namespace Payment.Registration.App.Builders
{
    public class PaymentPositionDtoBuilder : AbstractBuilder<PaymentPosition, PaymentPositionDto>, 
        IBuilder<PaymentPosition, PaymentPositionDto>
    {
        private readonly IBuilder<File, FileDto> fileDtoBuilder;

        public PaymentPositionDtoBuilder(IBuilder<File, FileDto> fileDtoBuilder)
        {
            this.fileDtoBuilder = fileDtoBuilder;
        }
        
        protected override void CreateMapping(IMappingExpression<PaymentPosition, PaymentPositionDto> cfg)
        {
            cfg
                .ForMember(ppDto => ppDto.Files,
                    expression => expression
                        .MapFrom(p => p.Files.Select(fileDtoBuilder.Build).ToArray()));
        }
    }
}