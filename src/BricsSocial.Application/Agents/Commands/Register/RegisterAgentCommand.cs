using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Vacancies.Commands.CreateVacancy;
using BricsSocial.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Agents.Commands.Register
{
    public sealed class RegisterAgentCommand : IRequest<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public string? Position { get; set; }
        public int? CompanyId { get; set; }
    }

    public sealed class RegisterAgentCommandHandler : IRequestHandler<RegisterAgentCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;

        public RegisterAgentCommandHandler(IApplicationDbContext context, IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        public async Task<int> Handle(RegisterAgentCommand request, CancellationToken cancellationToken)
        {
            var userInfo = new UserInfo
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = UserRoles.Agent,
                Email = request.Email,
            };
            (var result, var userId) = await _identityService.CreateUserAsync(userInfo, request.Password);

            if (!result.Succeeded)
                throw new Exception($"User creation error. {string.Join(", ", result.Errors)}");

            var agent = new Agent
            {
                IdentityId = userId,
                Position = request.Position,
                CompanyId = request.CompanyId.Value,
            };
            _context.Agents.Add(agent);
            await _context.SaveChangesAsync(cancellationToken);

            return agent.Id;
        }
    }
}
