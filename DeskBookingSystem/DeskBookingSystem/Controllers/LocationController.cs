using DeskBookingSystem.Data;
using DeskBookingSystem.Models;
using DeskBookingSystem.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeskBookingSystem.Controllers
{
    [Route("api/LocationAPI")]
    [ApiController]

    public class LocationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LocationController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult <IEnumerable<LocationDTO>> GetAllLocations()
        {
            return Ok(_context.Locations.ToList());
        }

        [HttpGet("{id:int}",Name ="GetLocation")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult<LocationDTO> GetLocation(int id)
        {
            if (id==0)
            {
                return BadRequest();
            }
            var location = _context.Locations.FirstOrDefault(x => x.Id == id);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<LocationDTO> CreateLocation([FromBody] LocationDTO locationDTO)
        {
            if (_context.Locations.FirstOrDefault(u=>u.LocationName.ToLower()== locationDTO.LocationName.ToLower())!= null)
            {
                ModelState.AddModelError("", "Location already exists!");
                return BadRequest(ModelState);
            }
            if (locationDTO == null)
            {
                return BadRequest(locationDTO);
            }
            if (locationDTO.Id> 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
            Location model = new()
            {
                Id = locationDTO.Id,
                LocationName = locationDTO.LocationName,
                Floor = locationDTO.Floor
            };
            _context.Locations.Add(model);
            _context.SaveChanges();
            return CreatedAtRoute("GetLocation",new { id = locationDTO.Id },locationDTO);
        }
        [HttpDelete("{id:int}",Name ="DeleteLocation")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult DeleteLocation(int id)
        {
            if (id==0)
            {
                return BadRequest();
            }
            var location = _context.Locations.FirstOrDefault(u=>u.Id==id);
            if (location == null)
            {
                return NotFound();
            }
            _context.Locations.Remove(location);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
