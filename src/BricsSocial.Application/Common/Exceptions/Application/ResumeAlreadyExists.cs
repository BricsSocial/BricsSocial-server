using BricsSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.Common.Exceptions.Application
{
    internal class ResumeAlreadyExists : Exception
    {
        public ResumeAlreadyExists(int specialistId, int resumeId)
            : base($"{nameof(Resume)} for {nameof(Specialist)} ({specialistId}) already exists ({resumeId}).")
        {
        }
    }
}
