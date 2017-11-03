﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Services.Common;
using DAL.Entities;

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