using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DataTransferObjects
{
    public class UserDto : ApplicantDto
    {
        public virtual List<SkillTagDto> Skills { get; set; }
    }
}