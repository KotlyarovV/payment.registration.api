using System;
using AutoMapper;
using Payment.Registration.App.DTOs;
using Payment.Registration.Domain.Models;

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
}