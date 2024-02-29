using Authorication.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorication.Domain.Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string ?FullName { get; set; }
        public string ?Login { get; set; }
        public string ?Password { get; set; }

        public Role Status { get; set; }
    }
}
