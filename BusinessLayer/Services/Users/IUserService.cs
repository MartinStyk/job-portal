using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Services.Common;

namespace BusinessLayer.Services.Users
{
    public interface IUserService : ICrudService<UserDto, UserFilterDto>
    {
        /// <summary>
        /// Gets user with given email address
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>User with given email address</returns>
        Task<UserDto> GetByEmailAsync(string email);


        /// <summary>
        /// Find all users matching filter criteria
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns>Users matching filter criteria</returns>
        Task<QueryResultDto<UserDto, UserFilterDto>> GetFiltered(UserFilterDto filter);

        Task<int> Create(UserCreateDto entityDto);

        (bool success, string roles) AuthorizeUserAsync(string email, string password);
    }
}