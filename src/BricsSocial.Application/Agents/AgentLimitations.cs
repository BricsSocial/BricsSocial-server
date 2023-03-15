using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Agents
{
    internal static class AgentLimitations
    {
        public const int FirstNameMinLength = 2;
        public const int FirstNameMaxLength = 100;
        public const int LastNameMinLength = 2;
        public const int LastNameMaxLength = 100;
        public const int PositionMinLength = 1;
        public const int PositionMaxLength = 200;
    }
}
