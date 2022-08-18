using AutoMapper;
using DeskBookingSystem.Data;
using DeskBookingSystem.Models;
using DeskBookingSystem.Models.DTOs;
using DeskBookingSystem.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DeskBookingSystem.Controllers
{
    [Route("api/LocationAPI")]
    [ApiController]

    public class LocationController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ILocationRepository _locationRepo;
        private readonly IMapper _mapper;
        public LocationController(ILocationRepository locationRepo, IMapper mapper)
        {
            _locationRepo = locationRepo;
            _mapper = mapper;
            this._response = new ();
        }
        [HttpGet]
        
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult <APIResponse>> GetAllLocations()
        {
            try
            {
                IEnumerable<Location> locations = await _locationRepo.GetAll();
                _response.Result = _mapper.Map<List<LocationDTO>>(locations);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSucces=false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}",Name ="GetLocation")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<APIResponse>> GetLocation(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var location = await _locationRepo.Get(x => x.Id == id);
                if (location == null)
                {
                    return NotFound();
                }
                _response.Result = _mapper.Map<LocationDTO>(location);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;

        }
        [HttpPost]
        [Authorize(Roles ="admin")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]

        public async Task<ActionResult<APIResponse>> CreateLocation([FromBody] LocationCreateDTO locationCreateDTO)
        {
            try
            {


                if (await _locationRepo.Get(u => u.LocationName.ToLower() == locationCreateDTO.LocationName.ToLower()) != null)
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
                _response.Result = _mapper.Map<LocationDTO>(model);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetLocation", new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete("{id:int}",Name ="DeleteLocation")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<APIResponse>> DeleteLocation(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var location = await _locationRepo.Get(u => u.Id == id);
                if (location == null)
                {
                    return NotFound();
                }
                await _locationRepo.Remove(location);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSucces = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
