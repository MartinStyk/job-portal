using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public IList<string> SkillNames { get; set; }

        public IList<string> QuestionTexts { get; set; }

        public JobOfferCreateDto()
        {
        }

        public JobOfferCreateDto(JobOfferDto jobOffer)
        {
            Id = jobOffer.Id;
            Name = jobOffer.Name;
            EmployerId = jobOffer.EmployerId;
            Location = jobOffer.Location;
            Description = jobOffer.Description;
            SkillNames = jobOffer.Skills;

            QuestionTexts = new List<string>();
            if(jobOffer.Questions != null)
                foreach (var question in jobOffer.Questions)
                {
                    QuestionTexts.Add(question.Text);
                }

        }
    }
}