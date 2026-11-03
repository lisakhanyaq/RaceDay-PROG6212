using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Data;
using RaceDay.Api.Models;

namespace RaceDay.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public CategoriesController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetCategoriesForEvent(int eventId)
        {
            var categories = await _context.Categories
                .Where(c => c.EventId == eventId)
                .ToListAsync();

            return Ok(categories);
        }

        [HttpPost("event/{eventId}")]
        public async Task<IActionResult> CreateCategory(
            int eventId,
            [FromBody] Category category)
        {
            var roleId = HttpContext.Session.GetInt32("RoleId");

            if (roleId == null)
                return Unauthorized("Not logged in");

            if (roleId.Value != 1)
                return Forbid();

            category.EventId = eventId;

            _context.Categories.Add(category);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCategoriesForEvent),
                new { eventId },
                category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(
            int id,
            [FromBody] Category category)
        {
            var roleId = HttpContext.Session.GetInt32("RoleId");

            if (roleId == null)
                return Unauthorized();

            if (roleId.Value != 1)
                return Forbid();

            var existing = await _context.Categories.FindAsync(id);

            if (existing == null)
                return NotFound("Category not found");

            existing.Name = category.Name;
            existing.Description = category.Description;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var roleId = HttpContext.Session.GetInt32("RoleId");

            if (roleId == null)
                return Unauthorized();

            if (roleId.Value != 1)
                return Forbid();

            var existing = await _context.Categories.FindAsync(id);

            if (existing == null)
                return NotFound("Category not found");

            _context.Categories.Remove(existing);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Category deleted successfully"
            });
        }
    }
}