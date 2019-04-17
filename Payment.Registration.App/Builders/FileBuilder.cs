using System;
using System.IO;
using AutoMapper;
using Payment.Registration.App.DTOs;
using File = Payment.Registration.Domain.Models.File;

namespace Payment.Registration.App.Builders
{
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
                            f => $"{Guid.NewGuid()}{Guid.NewGuid()}.{f.Extension}"));
        }
    }
}