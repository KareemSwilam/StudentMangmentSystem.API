using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Dtos.AuthDtos;
using Task2.Application.ExternalServices;
using Task2.Application.Result;
using Task2.Application.Services.IServices;

namespace Task2.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthRepository(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;

        }
        public async Task<CustomResult<string>> Register(RegistrationRequestDto registration)
        {
            var user = new IdentityUser()
            {
                UserName = registration.UserName,
                Email = registration.Email,
            };
            var result = await _userManager.CreateAsync(user, registration.Password);
            if (result.Succeeded)
            {
                var token = await GenerateToken(user);
                return CustomResult<string>.Success(token);
            }
            return CustomResult<string>.Failure(CustomError.ValidationError("Faild in Registeration")); //await result.Errors.Select(e => e.Description).ToString();
        }

        public async  Task<bool> UserExist(string Email)
        {
            var userexist = await _userManager.FindByEmailAsync(Email);
            if (userexist == null)
                return false;
            return true;
        }
        public  async Task<string> GenerateToken(IdentityUser user)
        {
            var claim = await  GetAllClaim(user);
            var key = _configuration.GetSection("JWTConfig:Secert").Value;
            var SymmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
            var Crendential = new SigningCredentials(SymmetricKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
            (
                claims: claim,
                signingCredentials: Crendential,
                expires: DateTime.Now.AddDays(1)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);


        }
        public async Task<List<Claim>> GetAllClaim(IdentityUser user)
        {
            var _option = new IdentityOptions();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email! ),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };
            var userCliams = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userCliams);
            var UserRole = await _userManager.GetRolesAsync(user);
            foreach(var userrole in UserRole)
            {
                var role = await _roleManager.FindByNameAsync(userrole);
                if(role != null)
                {
                    var roleClaim = await _roleManager.GetClaimsAsync(role);
                    claims.Add(new Claim(ClaimTypes.Role, userrole));
                    claims.AddRange(userCliams);
                }

            }
            return claims;
        }

        public async Task<CustomResult<string>> Login(LoginRequestDto loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if(user != null)
            {
                var correct = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
                if(correct)
                {
                    var token = await GenerateToken(user);
                    return CustomResult<string>.Success(token);
                }
                return CustomResult<string>.Failure(CustomError.ValidationError("InValid Email Or Password"));
            }
            return  CustomResult<string>.Failure(CustomError.ValidationError("InValid Email Or Password"));
        }
    }
}
