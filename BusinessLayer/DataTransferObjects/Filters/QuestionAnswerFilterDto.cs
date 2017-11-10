using BusinessLayer.DataTransferObjects.Common;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class QuestionAnswerFilterDto : FilterDtoBase
    {
        public int? ApplicationId { get; set; }
        public int? QuestionId { get; set; }
    }
}