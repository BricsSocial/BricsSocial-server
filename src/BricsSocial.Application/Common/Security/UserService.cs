using BricsSocial.Application.Common.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Common.Security
{
    public interface IUserService
    {
        void CheckUserIdentity(string userId, string identityId);
    }

    public sealed class UserService : IUserService
    {
        public void CheckUserIdentity(string userId, string identityId)
        {
            if (userId != identityId)
                throw new ForbiddenAccessException();
        }
    }
}
