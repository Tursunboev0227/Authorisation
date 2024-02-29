using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorication.Domain.Enums
{
    public enum Permission
    {
        GetAllUsers = 1,
        GetUsersById = 2,
        DeleteUser = 3,
        UpdateUser = 4,
        CreateUser = 5
    }
}
