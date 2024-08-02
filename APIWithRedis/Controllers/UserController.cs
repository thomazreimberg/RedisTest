using APIWithRedis.Domain.Entities;
using APIWithRedis.Domain.Interfaces.Repositories;
using APIWithRedis.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APIWithRedis.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Post([FromBody] User userDto)
        {
            try
            {
                var user = new User(userDto.Name, userDto.Username, userDto.Password);

                var result = await _userRepository.Add(user);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody] User userDto)
        {
            try
            {
                var user = await _userRepository.GetUserByUsername(userDto.Username);

                if (user == null) return NotFound(new { Message = "User or password not valid."});

                if (userDto.Password != user.Password) return NotFound(new { Message = "User or password not valid." });

                var token = TokenService.GenerateToken(user);

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        [Route("CreateAuthorize")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<dynamic>> PostAuthorize([FromBody] User userDto)
        {
            try
            {
                var user = new User(userDto.Name, userDto.Username, userDto.Password);

                var result = await _userRepository.Add(user);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
