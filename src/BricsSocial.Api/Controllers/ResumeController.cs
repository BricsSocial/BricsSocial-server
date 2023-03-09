using BricsSocial.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BricsSocial.Domain.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResumeController : ControllerBase
    {

        private readonly ILogger<ResumeController> _logger;

        public ResumeController(ILogger<ResumeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Resume> GetAll()
        {
            
        }
    }
}