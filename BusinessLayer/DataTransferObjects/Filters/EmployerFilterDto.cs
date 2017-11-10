using BusinessLayer.DataTransferObjects.Common;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class EmployerFilterDto : FilterDtoBase
    {
        public string Email { get; set; }

        public string Name { get; set; }

    }
}