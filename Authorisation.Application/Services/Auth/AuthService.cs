using Authorisation.Application.Services.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace MCorporation.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthService(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;

        }

        public async Task<string> GenerateToken(string login, string password)
        {
            if (login == null || password == null)
            {
                return "LogIn or Password is null. Please Enter Login and Password";

            }
            if (UserExist(login, password).Result)
            {
                var permission = new List<int>();
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
                if (user.Role == "Admin")
                {
                    permission = new List<int> { 1, 2, 3, 4, 5 };
                }
                else
                {
                    permission = new List<int> { 1, 2 };


                }
                var jsonContent = JsonSerializer.Serialize(permission);

                string gu = Guid.NewGuid().ToString();
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Role, string(user.Role)),
                    new Claim("UserFullName", user.FullName),
                    new Claim("TokenId", gu),
                    new Claim("CratedDate", DateTime.UtcNow.ToString()),
                    new Claim("Permissions", jsonContent)

                };

                return await GenerateToken(claims);

            }
            else return "User UnAuthorithe 401";
        }

        public async Task<string> GenerateToken(IEnumerable<Claim> additionalClaims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var exDate = Convert.ToInt32(_configuration["JWT:ExpireDate"] ?? "5");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64)
            };

            if (additionalClaims?.Any() == true)
            {
                claims.AddRange(additionalClaims);
            }

            var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(exDate),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<bool> UserExist(string login, string password)
        {
            var listUsers = await _context.Users.ToListAsync();
            foreach (var user in listUsers)
            {
                if (user.Login == login && user.Password == password) return true;
            }

            return false;
        }
    }
}
