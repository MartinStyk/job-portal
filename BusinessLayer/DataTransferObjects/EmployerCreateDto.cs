using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using BusinessLayer.DataTransferObjects.Common;

namespace BusinessLayer.DataTransferObjects
{
    public class EmployerCreateDto : EmployerDto
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}