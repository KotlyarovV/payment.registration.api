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
                    expression => expression.Ignore());
        }

        public override Applicant Build(ApplicantSaveDto saveDto)
        {
            var applicant = base.Build(saveDto);
            applicant.Id = Guid.NewGuid();
            return applicant;
        }

        public override void Map(ApplicantSaveDto saveDto, Applicant applicant)
        {
            base.Map(saveDto, applicant);
        }
    }
}