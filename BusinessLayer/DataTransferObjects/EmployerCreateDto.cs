using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DataTransferObjects
{
    public class EmployerCreateDto : EmployerDto
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}