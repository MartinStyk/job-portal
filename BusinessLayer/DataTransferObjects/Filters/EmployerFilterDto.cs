using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects.Common;

namespace BusinessLayer.DataTransferObjects.Filters
{
    public class EmployerFilterDto : FilterDtoBase
    {
        public string Email { get; set; }

        public string Name { get; set; }

    }
}