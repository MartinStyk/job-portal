using System;
using System.ComponentModel.DataAnnotations;
using BusinessLayer.DataTransferObjects.Common;

namespace BusinessLayer.DataTransferObjects
{
    public class JobOfferCreateDto : DtoBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int EmployerId { get; set; }

        public String Location { get; set; }

        public string Description { get; set; }

        public int[] SkillsIds { get; set; }

        public string[] QuestionTexts { get; set; }
    }
}