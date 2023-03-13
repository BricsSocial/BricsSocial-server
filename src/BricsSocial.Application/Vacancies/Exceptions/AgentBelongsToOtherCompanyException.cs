﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Vacancies.Exceptions
{
    public sealed class AgentBelongsToOtherCompanyException : Exception
    {
        public AgentBelongsToOtherCompanyException(object agentId, object agentCompanyId, object expectedCompanyId) // TODO: typed ids
            : base($"Agent belongs to other company than expected (agent = {agentId}, agent company = {agentCompanyId}, expected company = {expectedCompanyId})")
        {
        }
    }
}
