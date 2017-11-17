using System.ComponentModel.DataAnnotations;
using BusinessLayer.DataTransferObjects.Common;

namespace BusinessLayer.DataTransferObjects
{
    public class EmployerDto : DtoBase
    {
        [Required]
        [EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        
    }
}