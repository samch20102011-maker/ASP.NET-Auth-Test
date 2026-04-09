using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient; // ✅ added

namespace MyLoginApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin request)
        {
            using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            conn.Open();

            var cmd = new SqlCommand(
                "SELECT COUNT(*) FROM Auth.Users WHERE Username=@u AND Password=@p", conn);
            cmd.Parameters.AddWithValue("@u", request.Username);
            cmd.Parameters.AddWithValue("@p", request.Password);

            int count = (int)cmd.ExecuteScalar();

            if (count > 0)
                return Ok(new { message = "Login successful!" });
            else
                return Unauthorized(new { message = "Invalid credentials." });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserLogin request)
        {
            using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            conn.Open();

            var checkCmd = new SqlCommand(
                "SELECT COUNT(*) FROM Auth.Users WHERE Username=@u", conn);
            checkCmd.Parameters.AddWithValue("@u", request.Username);

            int exists = (int)checkCmd.ExecuteScalar();
            if (exists > 0)
                return BadRequest(new { message = "Username already exists." });

            var insertCmd = new SqlCommand(
                "INSERT INTO Auth.Users (Username, Password) VALUES (@u, @p)", conn);
            insertCmd.Parameters.AddWithValue("@u", request.Username);
            insertCmd.Parameters.AddWithValue("@p", request.Password);
            insertCmd.ExecuteNonQuery();

            return Ok(new { message = "Registration successful!" });
        }
    }

    public class UserLogin
    {
        public string? Username { get; set; } // ✅ nullable
        public string? Password { get; set; } // ✅ nullable
    }
}