using LoginMicroservice.Business.Interface;
using LoginMicroservice.Business.Model;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using LoginMicroservice.Data.Interface;

namespace LoginMicroservice.Business
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public LoginService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            // Validate credentials
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);
            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
            {
                throw new Exception("Invalid username or password.");
            }

            // Generate JWT token
            var token = GenerateJwtToken(user.Username);

            return new LoginResponse { Token = token };
        }

        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                new Claim[] { new Claim(ClaimTypes.Name, username) },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            // Implement password verification logic (e.g., using hashing libraries)
            // For simplicity, assume direct comparison in this example
            return password == passwordHash;
        }
    }

}
