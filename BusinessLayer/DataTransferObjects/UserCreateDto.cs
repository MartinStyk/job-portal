using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DataTransferObjects
{
    public class UserCreateDto : UserDto
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}