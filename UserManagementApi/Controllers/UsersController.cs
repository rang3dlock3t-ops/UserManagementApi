using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        private readonly IConfiguration _configuration;

        public UsersController(IUserRepository userRepository,IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _users = userRepository;
            _hasher = passwordHasher;
            _configuration = configuration;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var user = _users.GetAll().FirstOrDefault(u => u.UserEmail == dto.UserEmail);
            if (user == null) return Unauthorized("User not found");

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result != PasswordVerificationResult.Success) return Unauthorized("Invalid password");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.UserEmail),
                new Claim(ClaimTypes.Role, user.Role) 
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult GetAll() => Ok(_users.GetAll());

        [Authorize(Roles = "Admin")]
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
                UserEmail= userDto.UserEmail, PasswordHash= _hasher.HashPassword(null!,userDto.Password)};
            _users.Add(user);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateDto Update)
        {
            _users.Update(id, Update);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult DeleteById(int id)
        {
            _users.Delete(id);

            return NoContent();
        }
    }
}

