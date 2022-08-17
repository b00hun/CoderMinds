using AutoMapper;
using DeskBookingSystem.Models;
using DeskBookingSystem.Models.DTOs;
using DeskBookingSystem.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DeskBookingSystem.Controllers
{
    [Route("api/DeskAPI")]
    [ApiController]
    public class DeskController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IDeskRepository _deskRepo;
        private readonly ILocationRepository _locationRepo;
        private readonly IMapper _mapper;
        public DeskController(IDeskRepository deskRepo, IMapper mapper, ILocationRepository locationRepo)
        {
            _deskRepo = deskRepo;
            _mapper = mapper;
            this._response = new();
            _locationRepo = locationRepo;
        }
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllDesks()
        {
            try
            {
                IEnumerable<Desk> desks = await _deskRepo.GetAll();
                _response.Result = _mapper.Map<List<DeskDTO>>(desks);
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

        [HttpGet("{id:int}", Name = "GetDesk")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<APIResponse>> GetDesk(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var location = await _deskRepo.Get(x => x.Id == id);
                if (location == null)
                {
                    return NotFound();
                }
                _response.Result = _mapper.Map<DeskDTO>(location);
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
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<APIResponse>> CreateDesk([FromBody] DeskCreateDTO deskCreateDTO)
        {
            try
            {

                if (await _locationRepo.Get(u=>u.Id == deskCreateDTO.LocationID) == null)
                {
                    ModelState.AddModelError("Custom Error", "LocationID didn't exist");
                }
                if (await _deskRepo.Get(u => u.DeskName.ToLower() == deskCreateDTO.DeskName.ToLower()) != null)
                {
                    ModelState.AddModelError("Custom Error", "Desk already exists!");
                    return BadRequest(ModelState);
                }
                if (deskCreateDTO == null)
                {
                    return BadRequest(deskCreateDTO);
                }
                Desk model = _mapper.Map<Desk>(deskCreateDTO);

                await _deskRepo.Create(model);
                _response.Result = _mapper.Map<DeskDTO>(model);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetDesk", new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete("{id:int}", Name = "DeleteDesk")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<APIResponse>> DeleteDesk(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var location = await _deskRepo.Get(u => u.Id == id);
                if (location == null)
                {
                    return NotFound();
                }
                await _deskRepo.Remove(location);

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
