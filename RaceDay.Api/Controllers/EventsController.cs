using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Data;
using RaceDay.Api.Models;

namespace RaceDay.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public EventsController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _context.Events.ToListAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var ev = await _context.Events.FindAsync(id);

            if (ev == null)
                return NotFound("Event not found");

            return Ok(ev);
        }

        [HttpGet("mine")]
        public async Task<IActionResult> GetMyEvents()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return Unauthorized("Not logged in");

            var events = await _context.Events
                .Where(e => e.OrganiserId == userId.Value)
                .ToListAsync();

            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] Event ev)
        {
            var roleId = HttpContext.Session.GetInt32("RoleId");
            var userId = HttpContext.Session.GetInt32("UserId");

            if (roleId == null || userId == null)
                return Unauthorized("Not logged in");

            if (roleId.Value != 1)
                return Forbid();

            ev.OrganiserId = userId.Value;

            _context.Events.Add(ev);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEventById),
                new { id = ev.EventId },
                ev);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] Event ev)
        {
            var roleId = HttpContext.Session.GetInt32("RoleId");
            var userId = HttpContext.Session.GetInt32("UserId");

            if (roleId == null || userId == null)
                return Unauthorized();

            if (roleId.Value != 1)
                return Forbid();

            var existing = await _context.Events.FindAsync(id);

            if (existing == null)
                return NotFound("Event not found");

            if (existing.OrganiserId != userId.Value)
                return Forbid();

            existing.Name = ev.Name;
            existing.Description = ev.Description;
            existing.EventDate = ev.EventDate;
            existing.Location = ev.Location;
            existing.DistanceKm = ev.DistanceKm;
            existing.EventType = ev.EventType;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var roleId = HttpContext.Session.GetInt32("RoleId");
            var userId = HttpContext.Session.GetInt32("UserId");

            if (roleId == null || userId == null)
                return Unauthorized();

            if (roleId.Value != 1)
                return Forbid();

            var existing = await _context.Events.FindAsync(id);

            if (existing == null)
                return NotFound("Event not found");

            if (existing.OrganiserId != userId.Value)
                return Forbid();

            _context.Events.Remove(existing);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Event deleted successfully"
            });
        }
    }
}