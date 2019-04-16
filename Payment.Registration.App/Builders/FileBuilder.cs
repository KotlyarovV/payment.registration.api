using System;
using AutoMapper;
using Payment.Registration.App.DTOs;
using Payment.Registration.Domain.Models;

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
                            f => $"{Guid.NewGuid()}{Guid.NewGuid()}"));
        }
    }
}