using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Services.Common;
using DAL.Entities;

namespace BusinessLayer.Services.Employers
{
    public interface IEmployerService : ICrudService<EmployerDto, EmployerFilterDto>
    {
        /// <summary>
        /// Find employer by name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>Employer for given name</returns>
        Task<EmployerDto> GetByName(string name);

        /// <summary>
        /// Find employer by mail
        /// </summary>
        /// <param name="mail">mail</param>
        /// <returns>Employer for given mail</returns>
        Task<EmployerDto> GetByEmail(string mail);
    }
}