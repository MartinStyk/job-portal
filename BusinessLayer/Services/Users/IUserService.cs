using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Services.Common;
using DAL.Entities;

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

    }
}