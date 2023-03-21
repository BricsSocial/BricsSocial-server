using AutoMapper;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Models;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Specialists.Common;
using BricsSocial.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Specialists.CreateSpecialist
{
    public record CreateSpecialistCommand : IRequest<SpecialistDto>
    {
        public string? Email { get; init; }
        public string? Password { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public int? CountryId { get; init; }

    }

    public sealed class CreateSpecialistCommandHandler : IRequestHandler<CreateSpecialistCommand, SpecialistDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSpecialistCommandHandler> _logger;

        public CreateSpecialistCommandHandler(IApplicationDbContext context, IIdentityService identityService, IMapper mapper, ILogger<CreateSpecialistCommandHandler> logger)
        {
            _context = context;
            _identityService = identityService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<SpecialistDto> Handle(CreateSpecialistCommand request, CancellationToken cancellationToken)
        {
            var userInfo = new UserInfo
            {
                Role = UserRoles.Specialist,
                Email = request.Email
            };
            (var result, var userId) = await _identityService.CreateUserAsync(userInfo, request.Password);

            if (!result.Succeeded)
                throw new Exception($"User creation error. {string.Join(", ", result.Errors)}");

            var specialist = new Specialist
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdentityId = userId,
                CountryId = request.CountryId.Value,
            };
            
            try
            {
                _context.Specialists.Add(specialist);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"{nameof(Specialist)} creation error");

                try
                {
                    await _identityService.DeleteUserAsync(userId);
                }
                catch(Exception ex1)
                {
                    _logger.LogCritical(ex1, $"Cannot delete invalid User ({userId})");
                }

                throw;
            }

            return _mapper.Map<SpecialistDto>(specialist);
        }
    }
}
