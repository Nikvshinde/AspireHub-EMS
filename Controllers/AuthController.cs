using AspireHub_EMS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;
    public AuthController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpGet("check-admin")]
    public IActionResult CheckAdmin()
    {
        var adminExists = _context.Users.Include(u => u.Role)
                            .Any(u => u.Role.RoleName == "Admin");
        return Ok(new { adminExists });
    }


    [HttpPost("create-admin")]
    public IActionResult CreateAdmin([FromBody] RegisterDto dto)
    {
        var adminExists = _context.Users.Include(u => u.Role)
                             .Any(u => u.Role.RoleName == "Admin");
        if (adminExists) return BadRequest("Admin already exists");

        var adminRole = _context.Roles.FirstOrDefault(r => r.RoleName == "Admin");
        if (adminRole == null)
        {
            adminRole = new Role { RoleName = "Admin" };
            _context.Roles.Add(adminRole);
            _context.SaveChanges();
        }

        var admin = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            RoleId = adminRole.RoleId
        };
        _context.Users.Add(admin);
        _context.SaveChanges();
        return Ok(admin);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        var user = _context.Users.Include(u => u.Role)
                    .FirstOrDefault(u => u.Username == dto.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials");

        var token = GenerateJwtToken(user);
        return Ok(new { token, role = user.Role.RoleName });
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.RoleName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

// DTOs
public class RegisterDto { public string Username; public string Email; public string Password; }
public class LoginDto { public string Username; public string Password; }