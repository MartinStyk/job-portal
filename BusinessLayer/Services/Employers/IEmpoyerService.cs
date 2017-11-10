using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Services.Common;

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

        /// <summary>
        /// Find all employers matching filter criteria
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns>Employers matching filter criteria</returns>
        Task<QueryResultDto<EmployerDto, EmployerFilterDto>> GetFiltered(EmployerFilterDto filter);
    }
}