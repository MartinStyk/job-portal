using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Context;
using Infrastructure.Entity;

namespace DAL.Entities
{
    public class QuestionAnswer : IEntity
    {
        public int Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(JobPortalDbContext.QuestionAnswers);

        public string Text { get; set; }

        [Required]
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        [Required]
        public int ApplicationId { get; set; }

        public virtual JobApplication Application { get; set; }
    }
}