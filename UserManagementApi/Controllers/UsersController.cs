using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using UserManagementApi.Dto;
using UserManagementApi.DTO;
using UserManagementApi.Models;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _users;
        private readonly IPasswordHasher<User> _hasher;
        public UsersController(IUserRepository userRepository,IPasswordHasher<User> passwordHasher)
        {
            _users = userRepository;
            _hasher = passwordHasher; 
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_users.GetAll());


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _users.GetById(id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterDto userDto)
        {

            var user = new User{ UserName= userDto.UserName,
                UserEmail= userDto.UserEmail, Password= _hasher.HashPassword(null!,userDto.Password)};
            _users.Add(user);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateDto Update)
        {
            _users.Update(id, Update);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteById(int id)
        {
            _users.Delete(id);

            return NoContent();
        }
    }
}

