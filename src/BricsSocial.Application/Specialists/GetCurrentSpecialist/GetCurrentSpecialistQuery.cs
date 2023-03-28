using AutoMapper;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using BricsSocial.Application.Specialists.Common;
using BricsSocial.Application.Specialists.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Specialists.GetCurrentSpecialist
{
    [Authorize(Roles = UserRoles.Specialist)]
    public record GetCurrentSpecialistQuery : IRequest<SpecialistDto>
    {

    }

    public record GetCurrentSpecialistQueryHandler : IRequestHandler<GetCurrentSpecialistQuery, SpecialistDto>
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        private readonly ISpecialistService _specialistService;

        public GetCurrentSpecialistQueryHandler(IMapper mapper, ICurrentUserService currentUser, ISpecialistService specialistService)
        {
            _mapper = mapper;
            _currentUser = currentUser;
            _specialistService = specialistService;
        }

        public async Task<SpecialistDto> Handle(GetCurrentSpecialistQuery request, CancellationToken cancellationToken)
        {
            var specialist = await _specialistService.GetSpecialistByUserIdAsync(_currentUser.UserId);

            return _mapper.Map<SpecialistDto>(specialist);
        }
    }
}
