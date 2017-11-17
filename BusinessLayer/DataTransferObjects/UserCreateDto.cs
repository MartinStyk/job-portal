using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DataTransferObjects
{
    public class UserCreateDto : UserDto
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}