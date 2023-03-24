using BricsSocial.Application.Common.Exceptions.Application;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Specialists.Services
{
    public interface ISpecialistService
    {
        Task<Specialist> GetSpecialistByUserId(string userId);
    }

    public sealed class SpecialistService : ISpecialistService
    {
        private readonly IApplicationDbContext _context;

        public SpecialistService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Specialist> GetSpecialistByUserId(string userId)
        {
            var specialist = await _context.Specialists
                .AsNoTracking()
                .Where(a => a.IdentityId == userId)
                .FirstOrDefaultAsync();

            if (specialist is null)
                throw new SpecialistUserNotFound(userId);

            return specialist;
        }
    }
}
