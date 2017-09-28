using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class QuestionAnswer
    {
        public int Id { get; set; }

        public string Text { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public Question Question { get; set; }

        [Required]
        public int ApplicationId { get; set; }

        [Required]
        public JobApplication Application { get; set; }
    }
}