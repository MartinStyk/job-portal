using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects.Common;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class JobOfferFilterDto : FilterDtoBase
    {
        public string Name { get; set; }

        public int? EmployerId { get; set; }

        public int[] SkillIds { get; set; }

        public string[] SkillNames { get; set; }

    }
}