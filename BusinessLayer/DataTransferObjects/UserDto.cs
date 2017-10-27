using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DataTransferObjects
{
    public class UserDto : ApplicantDto
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual List<SkillTagDto> Skills { get; set; }
    }
}