using System.Collections.Generic;

namespace BusinessLayer.DataTransferObjects
{
    public class UserDto : ApplicantDto
    {
        public IList<string> Skills { get; set; }
    }
}