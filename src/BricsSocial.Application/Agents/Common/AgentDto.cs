using BricsSocial.Application.Common.Mappings;
using BricsSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Agents.Common
{
    public sealed class AgentDto : IMapFrom<Agent>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Position { get; set; }
        public string? Photo { get; set; }
        public int CompanyId { get; set; }
    }
}
