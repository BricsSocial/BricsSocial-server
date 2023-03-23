﻿using BricsSocial.Application.Common.Exceptions.Application;
using BricsSocial.Application.Common.Exceptions.Common;
using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Application.Common.Security;
using BricsSocial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Agents.Services
{
    public interface IAgentService
    {
        Task<Agent> GetAgentByUserId(string userId);
        Task<Agent> CheckAgentBelongsToCompany(string userId, int companyId);
    }

    public sealed class AgentService : IAgentService
    {
        private readonly IApplicationDbContext _context;

        public AgentService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Agent> CheckAgentBelongsToCompany(string userId, int companyId)
        {
            var agent = await GetAgentByUserId(userId);

            if (agent.CompanyId != companyId)
                throw new AgentBelongsToOtherCompany(agent.Id, agent.CompanyId, companyId);

            return agent;
        }

        public async Task<Agent> GetAgentByUserId(string userId)
        {
            var agent = await _context.Agents
                .AsNoTracking()
                .Where(a => a.IdentityId == userId)
                .FirstOrDefaultAsync();

            if (agent is null)
                throw new AgentUserNotFound(userId);

            return agent;
        }
    }
}