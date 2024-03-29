﻿using System;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.Users;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades
{
    public class UserFacade : FacadeBase
    {
        private readonly IUserService userService;

        public UserFacade(IUnitOfWorkProvider unitOfWorkProvider, IUserService userService)
            : base(unitOfWorkProvider)
        {
            this.userService = userService;
        }

        // TODO registration
        public async Task Register(UserCreateDto user)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                await userService.Create(user);
                await unitOfWork.Commit();
            }
        }

        public async Task Update(UserDto user, UserDto editor)
        {
            if(user.Id != editor.Id)
                throw new ArgumentException();

            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                await userService.Update(user);
                await unitOfWork.Commit();
            }
        }

        public async Task<UserDto> GetById(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await userService.GetAsync(id);
            }
        }

        public async Task<QueryResultDto<UserDto, UserFilterDto>> GetAllUsersAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await userService.ListAllAsync();
            }
        }

        public async Task<UserDto> GetUserByEmail(String email)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await userService.GetByEmailAsync(email);
            }
        }

        public async Task<QueryResultDto<UserDto, UserFilterDto>> GetUserForFilter(
            UserFilterDto userFilterDto)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await userService.GetFiltered(userFilterDto);
            }
        }

        public async Task DeleteUser(int id)
        {
            using (var unitOfWork  = UnitOfWorkProvider.Create())
            {
                userService.Delete(id);
                await unitOfWork.Commit();
            }
        }

        public (bool success, string roles) Login(string mail, string password)
        {
            using (UnitOfWorkProvider.Create())
            {
                return userService.AuthorizeUserAsync(mail, password);
            }
        }
    }
}