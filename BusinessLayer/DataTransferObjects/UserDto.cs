using System.Collections.Generic;

namespace BusinessLayer.DataTransferObjects
{
    public class UserDto : ApplicantDto
    {
        public virtual List<SkillTagDto> Skills { get; set; }
    }
}