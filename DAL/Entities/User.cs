using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class User : Applicant
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual List<SkillTag> Skills { get; set; }
    }
}