using BiocomWebApp.Database;
using BiocomWebApp.Database.Entity;
using BiocomWebApp.DTO;
using BiocomWebApp.DTO.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BiocomWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiagnosticsController : ControllerBase
    {
        private readonly BiocomContext _context;

        private readonly ILogger<DiagnosticsController> _logger;

        public DiagnosticsController(ILogger<DiagnosticsController> logger, BiocomContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public IActionResult Post(DTO.Diagnostic data)
        {
            try
            {
                var diagnostic = data.Map(_context);
                _context.Diagnostics.Add(diagnostic);
                _context.SaveChanges();

                return Ok();
            }
            catch(Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult Get(int userId)
        {
            var diagnostic = _context.Diagnostics
                .Include(d => d.Diets)
                .FirstOrDefault(d => d.UserId == userId && d.DateTime == _context.Diagnostics.Max(t => t.DateTime));
                

            if (diagnostic == null) {
                return NotFound(userId);
            }

            return Ok(diagnostic.Map(_context));
        }
    }
}
