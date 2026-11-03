using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Data;
using RaceDay.Api.Models;

namespace RaceDay.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public ResultsController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateResult([FromBody] Result result)
        {
            var roleId = HttpContext.Session.GetInt32("RoleId");

            if (roleId == null)
                return Unauthorized("Not logged in");

            if (roleId.Value != 1)
                return Forbid();

            _context.Results.Add(result);

            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpGet("mine")]
        public async Task<IActionResult> GetMyResults()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return Unauthorized("Not logged in");

            var results = await _context.Results
                .Include(r => r.Enrolment)
                .ThenInclude(e => e.Category)
                .Where(r => r.Enrolment.UserId == userId.Value)
                .ToListAsync();

            return Ok(results);
        }

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetResultsForEvent(int eventId)
        {
            var roleId = HttpContext.Session.GetInt32("RoleId");

            if (roleId == null)
                return Unauthorized("Not logged in");

            if (roleId.Value != 1)
                return Forbid();

            var results = await _context.Results
                .Include(r => r.Enrolment)
                .ThenInclude(e => e.User)
                .Include(r => r.Enrolment.Category)
                .Where(r => r.Enrolment.Category.EventId == eventId)
                .ToListAsync();

            return Ok(results);
        }
    }
}