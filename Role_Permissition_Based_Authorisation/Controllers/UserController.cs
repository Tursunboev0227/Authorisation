using Authorication.Domain.Entities.Models;
using Authorication.Domain.Enums;
using Authorisation.Application.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Role_Permissition_Based_Authorisation.API.Attributes;

namespace Role_Permissition_Based_Authorisation.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _usersService;

        public UsersController(IUserService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [IdentityFilter(Permission.GetAllUsers)]
        public Task<List<User>> GetAllUsers()
        {
            var res = _usersService.GetAllUsers();
            return res;
        }
        [HttpGet]
        [IdentityFilter(Permission.GetUsersById)]
        public Task<User> GetUsersById(int id)
        {
            Task<User>? res = _usersService.GetUserById(id);
            return res;
        }
        [HttpPost]
        [IdentityFilter(Permission.CreateUser)]
        public Task<string> CreateUser(User user)
        {
            Task<string> res = _usersService.CreateUser(user);
            return res;
        }
        [HttpPut]
        [IdentityFilter(Permission.UpdateUser)]
        public Task<string> UpdateUser(int id, User user)
        {
            var res = _usersService.UpdateUser(id, user);
            return res;
        }
        [HttpDelete]
        [IdentityFilter(Permission.DeleteUser)]
        public Task<string> DeleteUser(int id)
        {
            Task<string> res = _usersService.DeleteUser(id);
            return res;
        }
    }

}
