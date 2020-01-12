using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OpenTraumaRegistry.Api.Models;
using OpenTraumaRegistry.Data;
using OpenTraumaRegistry.Data.Models;
using OpenTraumaRegistry.Shared;

namespace OpenTraumaRegistry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration config;
        private readonly Context context;

        public LoginController(IConfiguration _config, Context _context)
        {
            config = _config;
            context = _context;
        }

        public async Task<ActionResult<string>> Login(string email, string password)
        {

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(email))
                return Unauthorized();

            Models.LoginModel login = new Models.LoginModel();
            login.Email = email;
            login.Password = password;

            User user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenStr = GenerateJSONWebToken(user);
                return tokenStr;
            }

            return Unauthorized();
        }

        private string GenerateJSONWebToken(User user)
        {
            try
            { 
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {  
                    new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress), 
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) 
                };
           
                var token = new JwtSecurityToken(
                    issuer: config["Jwt:Issuer"],
                    audience: config["Jwt:Issuer"],
                    claims,
                
                    expires: DateTime.Now.AddMinutes(Convert.ToInt32(config["Jwt:Expires"])),
                    signingCredentials: credentials);

                var encodedtoken = new JwtSecurityTokenHandler().WriteToken(token);
                return encodedtoken;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private User AuthenticateUser(LoginModel login)
        {
            try
            {
                User user = null;
                PasswordHelper passwordHasher = new PasswordHelper();
 
                user = context.Users.Where(u => u.EmailAddress == login.Email).FirstOrDefault();
                if(user != null)
                {      
                    if((!user.Locked) && (passwordHasher.AuthenticatePassword(login.Password, user.Password)))
                    {
                        user.LoginAttempts = 0;
                        context.SaveChanges();
                        return user; 
                    }
                    else
                    {
                        user.LoginAttempts = user.LoginAttempts++;
                        if(user.LoginAttempts >= 5)
                        {
                            user.Locked = true;
                        }
                        context.SaveChanges();
                        return null; 
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }
    }
}