using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessLayer.DataTransferObjects.Common;

namespace BusinessLayer.DataTransferObjects
{
    public class JobOfferDto : DtoBase, IEquatable<JobOfferDto>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int EmployerId { get; set; }

        public EmployerDto Employer { get; set; }

        public String Location { get; set; }

        public string Description { get; set; }

        public virtual List<string> Skills { get; set; }

        public virtual List<QuestionDto> Questions { get; set; }

        public bool Equals(JobOfferDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && EmployerId == other.EmployerId && Equals(Employer, other.Employer) && string.Equals(Location, other.Location) && string.Equals(Description, other.Description) && Equals(Skills, other.Skills) && Equals(Questions, other.Questions);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((JobOfferDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ EmployerId;
                hashCode = (hashCode * 397) ^ (Employer != null ? Employer.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Location != null ? Location.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Skills != null ? Skills.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Questions != null ? Questions.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}