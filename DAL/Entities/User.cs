using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class User : Applicant, ISoftDeleteEntity
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public virtual List<SkillTag> Skills { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}