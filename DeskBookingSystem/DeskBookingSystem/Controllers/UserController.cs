using DeskBookingSystem.Models;
using DeskBookingSystem.Models.DTOs;
using DeskBookingSystem.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DeskBookingSystem.Controllers
{
    [Route("api/UsersAuth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        protected APIResponse _response;
        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
            this._response = new();
        }


        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _userRepo.Login(model);
            if (loginResponse.User==null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSucces = false;
                _response.ErrorMessages.Add("Username or password is incorrect");
                return BadRequest(_response);
            }
            _response.StatusCode=HttpStatusCode.OK;
            _response.IsSucces = true;
            _response.Result = loginResponse;
            return Ok(_response);

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
        {
            bool isUserNameUnique = _userRepo.IsUnique(model.UserName);
            if (!isUserNameUnique)
            {
                _response.StatusCode=HttpStatusCode.BadRequest;
                _response.IsSucces=false;
                _response.ErrorMessages.Add("Username already exists");
                return BadRequest(_response);
            }
            var user = await _userRepo.Register(model);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSucces = false;
                _response.ErrorMessages.Add("Error while registering");
                return BadRequest(_response);

            }
            _response.StatusCode= HttpStatusCode.OK;
            _response.IsSucces= true;
            return Ok(_response);
        }
        
    }
}
