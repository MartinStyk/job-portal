using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using DAL.Entities;
using Infrastructure.Query;

namespace BusinessLayer.Configuration
{
    public class MappingConfiguration
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Applicant, ApplicantDto>().ReverseMap();
            config.CreateMap<Employer, EmployerDto>().ReverseMap();
            config.CreateMap<Employer, EmployerCreateDto>().ReverseMap();
            config.CreateMap<User, UserCreateDto>();
            config.CreateMap<UserCreateDto, User>().ForMember(dest => dest.Skills, opt => opt.Ignore());
            config.CreateMap<User, UserDto>();
            config.CreateMap<UserDto, User>().ForMember(dest => dest.Skills, opt => opt.Ignore());

            config.CreateMap<JobApplication, JobApplicationDto>();
            config.CreateMap<JobApplicationDto, JobApplication>().ForMember(dest => dest.JobOffer, opt => opt.Ignore());

            config.CreateMap<JobOffer, JobOfferDto>();
            config.CreateMap<JobOfferDto, JobOffer>().ForMember(dest => dest.Employer, opt => opt.Ignore());

            config.CreateMap<JobOfferCreateDto, JobOffer>().ReverseMap();

            config.CreateMap<QuestionAnswer, QuestionAnswerDto>();
            config.CreateMap<QuestionAnswerDto, QuestionAnswer>().ForMember(dest => dest.Question, opt => opt.Ignore());

            config.CreateMap<Question, QuestionDto>().ReverseMap();
            config.CreateMap<SkillTag, SkillTagDto>().ReverseMap();

            config.CreateMap<QueryResult<Applicant>, QueryResultDto<Applicant, ApplicantFilterDto>>();
            config.CreateMap<QueryResult<Employer>, QueryResultDto<EmployerDto, EmployerFilterDto>>();
            config.CreateMap<QueryResult<JobApplication>, QueryResultDto<JobApplicationDto, JobApplicationFilterDto>>();
            config.CreateMap<QueryResult<JobOffer>, QueryResultDto<JobOfferDto, JobOfferFilterDto>>();
            config.CreateMap<QueryResult<Question>, QueryResultDto<QuestionDto, QuestionFilterDto>>();
            config.CreateMap<QueryResult<QuestionAnswer>, QueryResultDto<QuestionAnswerDto, QuestionAnswerFilterDto>>();
            config.CreateMap<QueryResult<SkillTag>, QueryResultDto<SkillTagDto, SkillTagFilterDto>>();
            config.CreateMap<QueryResult<User>, QueryResultDto<UserDto, UserFilterDto>>();
        }
    }
}