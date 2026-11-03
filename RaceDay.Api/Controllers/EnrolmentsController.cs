using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Data;
using RaceDay.Api.Models;

namespace RaceDay.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrolmentsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public EnrolmentsController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnrolment([FromBody] Enrolment enrolment)
        {
            var roleId = HttpContext.Session.GetInt32("RoleId");

            if (roleId == null)
                return Unauthorized("Not logged in");

            if (roleId.Value != 2)
                return Forbid();

            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return Unauthorized("Not logged in");

            enrolment.UserId = userId.Value;

            _context.Enrolments.Add(enrolment);
            await _context.SaveChangesAsync();

            return Ok(enrolment);
        }

        [HttpGet("mine")]
        public async Task<IActionResult> GetMyEnrolments()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return Unauthorized("Not logged in");

            var enrolments = await _context.Enrolments
                .Include(e => e.Category)
                .Where(e => e.UserId == userId.Value)
                .ToListAsync();

            return Ok(enrolments);
        }

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetEnrolmentsForEvent(int eventId)
        {
            var roleId = HttpContext.Session.GetInt32("RoleId");

            if (roleId == null)
                return Unauthorized("Not logged in");

            if (roleId.Value != 1)
                return Forbid();

            var enrolments = await _context.Enrolments
                .Include(e => e.User)
                .Include(e => e.Category)
                .Where(e => e.Category.EventId == eventId)
                .ToListAsync();

            return Ok(enrolments);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateEnrolmentStatus(int id, [FromBody] string status)
        {
            var roleId = HttpContext.Session.GetInt32("RoleId");

            if (roleId == null)
                return Unauthorized();

            if (roleId.Value != 1)
                return Forbid();

            var enrolment = await _context.Enrolments.FindAsync(id);

            if (enrolment == null)
                return NotFound("Enrolment not found");

            enrolment.Status = status;

            await _context.SaveChangesAsync();

            return Ok(enrolment);
        }
    }
}