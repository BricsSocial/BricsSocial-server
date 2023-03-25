using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Auth.Current
{
    public sealed class CurrentUserDto
    {
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
