using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Requests.AppUser;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly postgresContext _db;

        public AppUsersController(postgresContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.AppUsers);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _db.AppUsers.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Register([FromBody] AppUserRegisterRequest request)
        {
            // Validate the registration request
            if (request == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid registration request");
            }

            // Check if the username or email is already in use
            if (_db.AppUsers.Any(u => u.Username == request.Username || u.Email == request.Email))
            {
                return BadRequest("Username or email already in use");
            }

            // TODO: Implement your user creation logic, hash the password, etc.
            // For simplicity, let's assume you have a method CreateUser in your postgresContext
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var newUser = _db.AppUsers.Add(new AppUser
            {
                Username = request.Username,
                Email = request.Email,
                Password = hashedPassword
            }).Entity;
            _db.SaveChanges();

            // TODO: Issue a JWT token for the registered user
            var token = GenerateJwtToken(newUser);

            // Return success status along with the JWT token
            return Ok(new { Status = "User registered successfully", Token = token });
        }

        //httppost for login, it takes in AppUserLoginRequest and returns a JWT token if the user is found in the database and the password is correct 
        [HttpPost("login")]
        public IActionResult Login([FromBody] AppUserLoginRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid login request");
            }

            var user = _db.AppUsers.FirstOrDefault(u => u.Username == request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return Unauthorized("Invalid username or password");
            }

            var token = GenerateJwtToken(user);
            return Ok(new { Status = "User logged in successfully", Token = token });


        }

        

        private object GenerateJwtToken(AppUser newUser)
        {

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("uGgzpXr2ak8vC5EzW2Zy6bFgH8TqJwMn");

           var tokenDescriptor = new SecurityTokenDescriptor
           {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, newUser.Username),
                    new Claim(ClaimTypes.Email, newUser.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
