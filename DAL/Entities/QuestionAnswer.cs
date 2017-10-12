using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class QuestionAnswer : IEntity<int>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public virtual Question Question { get; set; }

        [Required]
        public int ApplicationId { get; set; }

        [Required]
        public virtual JobApplication Application { get; set; }
    }
}