using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Entities;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using Infrastructure.Query;
using Infrastructure.Repository;


namespace BusinessLayer.Services.JobOffers
{
    public class QuestionAnswerService : CrudQueryServiceBase<QuestionAnswer, QuestionAnswerDto, QuestionAnswerFilterDto>, IQuestionAnswerService
    {
        public QuestionAnswerService(IMapper mapper, IRepository<QuestionAnswer> repository,
            QueryObjectBase<QuestionAnswerDto, QuestionAnswer, QuestionAnswerFilterDto, IQuery<QuestionAnswer>> quoryObject)
            : base(mapper, repository, quoryObject)
        {
        }

        protected override async Task<QuestionAnswer> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }
        
        public async Task<IEnumerable<QuestionAnswerDto>> GetByQuestion(int questionId)
        {
            var queryResult = await Query.ExecuteQuery(new QuestionAnswerFilterDto { QuestionId =  questionId});
            return queryResult.Items;
        }

        public async Task<IEnumerable<QuestionAnswerDto>> GetByApplication(int applicationId)
        {
            var queryResult = await Query.ExecuteQuery(new QuestionAnswerFilterDto { ApplicationId = applicationId });
            return queryResult.Items;
        }

        public async Task<QuestionAnswerDto> GetByApplicationQuestion(int applicationId, int questionId)
        {
            var queryResult = await Query.ExecuteQuery(new QuestionAnswerFilterDto { ApplicationId = applicationId, QuestionId = questionId});
            return queryResult.Items.SingleOrDefault();
        }
    }
}