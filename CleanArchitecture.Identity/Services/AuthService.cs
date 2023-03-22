using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Identity.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) throw new Exception($"El usuario con email {request.Email} no existe");

            var result = await _signInManager.PasswordSignInAsync(user.Email, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded) throw new Exception($"Las credenciales de acceso no son correctas, intenta de nuevo");

            return new AuthResponse
            {
                UserName = user.UserName,
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(await GenerateToken(user)),
                Email = user.Email
            };
        }

        public async Task<AuthResponse> Register(RegistationRequest request)
        {
            ApplicationUser existUser = await _userManager.FindByNameAsync(request.UserName);
            if (existUser != null) throw new Exception($"El user name ya existe");
            
            existUser = await _userManager.FindByEmailAsync(request.Email);
            if (existUser != null) throw new Exception($"Este email ya existe ya existe");

            ApplicationUser user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.UserName,
                Name = request.Name,
                LastName = request.Lastname,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) throw new Exception($"No se pudo crear el registro.");

            await _userManager.AddToRoleAsync(user, "Operator");
            return new AuthResponse
            {
                UserName = user.UserName,
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(await GenerateToken(user)),
                Email = user.Email
            };
        }


        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            List<Claim> roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)

            }.Union(userClaims).Union(roleClaims);

            SymmetricSecurityKey symmetricSeurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSeurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claim,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                    signingCredentials: signingCredentials
                );

            return jwtSecurityToken;


        }

    }
}
