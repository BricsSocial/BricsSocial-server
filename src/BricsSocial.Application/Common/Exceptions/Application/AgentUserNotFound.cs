﻿using BricsSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Common.Exceptions.Application
{
    public sealed class AgentUserNotFound : Exception
    {
        public AgentUserNotFound(string userId)
            : base($"{nameof(Agent)} not found for user {userId}.")
        {
        }
    }
}
