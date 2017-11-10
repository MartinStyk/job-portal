using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Services.Common;

namespace BusinessLayer.Services.Questions
{
    public interface IQuestionService : ICrudService<QuestionDto, QuestionFilterDto>
    {
        /// <summary>
        /// Find all questions containing some of specified words 
        /// </summary>
        /// <param name="words">words</param>
        /// <returns>Questions containing at last one of specified words</returns>
        Task<IEnumerable<QuestionDto>> GetByWords(string[] words);

    }
}