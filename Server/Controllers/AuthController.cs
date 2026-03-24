using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.DTOs.AuthDTO;
using Server.Helpers;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly JwtHelper _jwtHelper;

        public AuthController(AppDbContext dbContext, JwtHelper jwtHelper)
        {
            _dbContext = dbContext;
            _jwtHelper = jwtHelper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == dto.UserName);

            if (user == null)
            {
                return BadRequest("User doesn't exist");
            }

            //bool isMatch = BCrypt.Net.BCrypt.Verify(dto.Password, user.HashPassword);

            if (!VerifyPassword(dto.Password, user.HashPassword))
            {
                return BadRequest("UserName or Password Invalid");
            }

            List<string> roles = ["Admin", "User", "Manager"];
            string token = _jwtHelper.GenerateToken(user, roles);

            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (await _dbContext.Users.AnyAsync(u => u.UserEmail == dto.Email))
            {
                return BadRequest("User Already Exists");
            }

            var user = new User
            {
                UserName = dto.UserName,
                UserEmail = dto.Email,
                HashPassword = HashPassword(dto.Password)
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return Ok(user);
        }

        [Authorize]
        [HttpGet("test")]
        public string Test()
        {
            return "It is working";
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string text, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(text, hash);
        }
    }
}
