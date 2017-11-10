using BusinessLayer.DataTransferObjects.Common;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class QuestionFilterDto : FilterDtoBase
    {
        public string[] Keywords { get; set; }

    }
}
