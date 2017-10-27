using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessLayer.DataTransferObjects.Common;

namespace BusinessLayer.DataTransferObjects
{
    public class JobOfferDto : DtoBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int EmployerId { get; set; }

        public EmployerDto Employer { get; set; }

        public String Location { get; set; }

        public string Description { get; set; }

        public virtual List<SkillTagDto> Skills { get; set; }

        public virtual List<QuestionDto> Questions { get; set; }
        
    }
}