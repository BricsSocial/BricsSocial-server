﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Common.Models
{
    public sealed class UserInfo
    {
        public string UserId { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
