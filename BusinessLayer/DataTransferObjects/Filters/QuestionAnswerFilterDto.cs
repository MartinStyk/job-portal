using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects.Common;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class QuestionAnswerFilterDto : FilterDtoBase
    {
        public int? ApplicationId { get; set; }
        public int? QuestionId { get; set; }
    }
}