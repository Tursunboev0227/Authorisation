using Authorication.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorisation.Application.Services.Users
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<string> DeleteUser(int id);
        Task<string> UpdateUser(int id, User user);
        Task<string> CreateUser(User user);
    }
}
