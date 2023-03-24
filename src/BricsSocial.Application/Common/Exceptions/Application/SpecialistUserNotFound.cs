using BricsSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Common.Exceptions.Application
{
    public sealed class SpecialistUserNotFound : Exception
    {
        public SpecialistUserNotFound(string userId)
            : base($"{nameof(Specialist)} not found for user {userId}.")
        {
        }
    }
}
