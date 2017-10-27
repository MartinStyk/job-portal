using System.ComponentModel.DataAnnotations;
using BusinessLayer.DataTransferObjects.Common;

namespace BusinessLayer.DataTransferObjects
{
    public class SkillTagDto : DtoBase
    {
        [Required]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}