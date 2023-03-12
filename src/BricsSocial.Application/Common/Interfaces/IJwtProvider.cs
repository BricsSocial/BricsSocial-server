using BricsSocial.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Common.Interfaces
{
    public interface IJwtProvider
    {
        public string Generate(UserInfo userInfo);
    }
}
