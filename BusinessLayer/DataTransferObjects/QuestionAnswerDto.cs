using System.ComponentModel.DataAnnotations;
using BusinessLayer.DataTransferObjects.Common;

namespace BusinessLayer.DataTransferObjects
{
    public class QuestionAnswerDto : DtoBase
    {
        [Required]
        public string Text { get; set; }

        public int QuestionId { get; set; }

        public QuestionDto Question { get; set; }

        public int ApplicationId { get; set; }
    }
}