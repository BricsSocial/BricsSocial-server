using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Common.Security
{
    public static class UserRoles
    {
        public const string Administrator = "Administrator";
        public const string Agent = "Agent";
        public const string Specialist = "Specialist";

        public const string AdministratorAndAgent = $"{Administrator},{Agent}";
        public const string AdministratorAndSpecialist = $"{Administrator},{Specialist}";
        public const string AgentAndSpecialist = $"{Agent},{Specialist}";
    }
}
