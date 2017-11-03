using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Services.Common;
using DAL.Entities;

namespace BusinessLayer.Services.JobOffers
{
    public interface IQuestionAnswerService : ICrudService<QuestionAnswerDto, QuestionAnswerFilterDto>
    {
        /// <summary>
        /// Find all answers for given question
        /// </summary>
        /// <param name="questionId">questionId</param>
        /// <returns>Answers for given question</returns>
        Task<IEnumerable<QuestionAnswerDto>> GetByQuestion(int questionId);

        /// <summary>
        /// Find all answers for given application
        /// </summary>
        /// <param name="applicationId">applicationId</param>
        /// <returns>Answers for given application</returns>
        Task<IEnumerable<QuestionAnswerDto>> GetByApplication(int applicationId);

        /// <summary>
        /// Find answers for given question and application
        /// </summary>
        /// <param name="applicationId">applicationId</param>
        /// <param name="questionId">questionId</param>
        /// <returns>Answers for given question and application</returns>
        Task<QuestionAnswerDto> GetByApplicationQuestion(int applicationId, int questionId);
    }
}