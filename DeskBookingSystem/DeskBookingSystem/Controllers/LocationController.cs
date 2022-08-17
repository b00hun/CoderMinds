using AutoMapper;
using DeskBookingSystem.Data;
using DeskBookingSystem.Models;
using DeskBookingSystem.Models.DTOs;
using DeskBookingSystem.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeskBookingSystem.Controllers
{
    [Route("api/LocationAPI")]
    [ApiController]

    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepo;
        private readonly IMapper _mapper;
        public LocationController(ILocationRepository locationRepo, IMapper mapper)
        {
            _locationRepo = locationRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult <IEnumerable<LocationDTO>>> GetAllLocations()
        {
            IEnumerable<Location> locations = await _locationRepo.GetAll();
            return Ok(_mapper.Map<List<LocationDTO>>(locations));
        }

        [HttpGet("{id:int}",Name ="GetLocation")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<LocationDTO>> GetLocation(int id)
        {
            if (id==0)
            {
                return BadRequest();
            }
            var location = await _locationRepo.Get(x => x.Id == id);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<LocationDTO>(location));
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
       
        public async Task<ActionResult<LocationCreateDTO>> CreateLocation([FromBody] LocationCreateDTO locationCreateDTO)
        {
            if (await _locationRepo.Get(u=>u.LocationName.ToLower()== locationCreateDTO.LocationName.ToLower())!= null)
            {
                ModelState.AddModelError("", "Location already exists!");
                return BadRequest(ModelState);
            }
            if (locationCreateDTO == null)
            {
                return BadRequest(locationCreateDTO);
            }
            Location model = _mapper.Map<Location>(locationCreateDTO);

            await _locationRepo.Create(model);
            return CreatedAtRoute("GetLocation",new { id = model.Id },model);
        }
        [HttpDelete("{id:int}",Name ="DeleteLocation")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            if (id==0)
            {
                return BadRequest();
            }
            var location = await _locationRepo.Get(u=>u.Id==id);
            if (location == null)
            {
                return NotFound();
            }
            await _locationRepo.Remove(location);
            return NoContent();
        }
    }
}
