using API.Dtos;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;


        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<User>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(string id)
        {
            try
            {
                var existingUser = await _userService.GetUserByIdAsync(id);

                if (existingUser == null)
                {
                    return NotFound("User does not exist");
                }
                return Ok(existingUser);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var newUser = await _userService.Register(registerDto);

                return Ok(_mapper.Map<UserDto>(newUser));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto loginDto)
        {
            var token = await _userService.Login(loginDto);

            if (token == null) return Unauthorized();

            return Ok(token);


        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> EditUser([FromRoute] string id, [FromBody] UserDto userDto)
        {
            try
            {
                var existingUser = await _userService.GetUserByIdAsync(id);
                if (existingUser == null)
                    return NotFound("User does not exist");

                var updatedUser = await _userService.UpdateUserAsync(existingUser.Id, userDto);

                return Ok(updatedUser);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute] string id)
        {
            try
            {
                var existingUser = await _userService.GetUserByIdAsync(id);

                if (existingUser == null)
                    return NotFound("User does not exist");

                await _userService.DeleteUserAsync(id);

                return Ok(existingUser);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
