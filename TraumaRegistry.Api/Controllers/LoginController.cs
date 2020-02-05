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
        SecurityHelper passwordHasher;

        public LoginController(IConfiguration _config, Context _context)
        {
            config = _config;
            context = _context;
            passwordHasher = new SecurityHelper(config);
        }

        [ActionName("Login")]
        [HttpGet("{email}/{password}/{confirmationToken}")]
        public ActionResult<User> Login(string email, string password, string confirmationToken)
        {

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(email))
                return Unauthorized();

            Models.LoginModel login = new Models.LoginModel();
            login.Email = email;
            login.Password = password;
            login.ConfirmationToken = confirmationToken;

            User user = AuthenticateUser(login);
            user.Password = "";

            if (user != null && user.Authenticated)
            {
                user.jsonToken = GenerateJSONWebToken(user);
                return user;
            }
            else
            {
                return Unauthorized();
            }
        }
        [ActionName("ResetPassword")]
        [HttpGet("{email}/{password}/{newPassword}/{confirmationToken}")]
        public ActionResult<User> ResetPassword(string email, string password, string newPassword, string confirmationToken = "")
        {

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(email))
                return Unauthorized();

            Models.LoginModel login = new Models.LoginModel();
           
            login.Email = email;
            login.Password = password;
            login.ConfirmationToken = confirmationToken;

            User user = context.Users.Where(u => u.EmailAddress == email).FirstOrDefault();


            if (user != null &&
                !user.Locked &&
                user.ConfirmationToken == confirmationToken &&
                user.ConfirmationTokenExpires > DateTime.Now &&
                passwordHasher.AuthenticatePassword(password, user.Password))
            {
                user.Authenticated = false;
                if (passwordHasher.ValididatePasswordFormat(newPassword))
                {
                    user.Password = passwordHasher.Hash(newPassword);
                    user.jsonToken = GenerateJSONWebToken(user);
                    user.PasswordExpires = DateTime.Now.AddDays(90); //TODO: make this a setting.
                    user.Locked = false;
                    user.ForcePasswordReset = false;
                    user.ConfirmationToken = "";
                    user.LoginAttempts = 0;
                    user.LastLogin = DateTime.Now;
                    context.SaveChanges();
                    user.Authenticated = true;
                    user.message = "Password updated successfully.";
                }
                else
                {
                    user.Authenticated = false;
                    user.message = "Invalid password format.";
                }

                return user;
            }
            else
            {
                if (user != null)
                {
                    FailedLoginAttempt(user);
                }
                return Unauthorized();
            }
        }


        private void FailedLoginAttempt(User user)
        {
            user.LoginAttempts = user.LoginAttempts++;
            context.SaveChanges();
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
                
 
                user = context.Users.Where(u => u.EmailAddress == login.Email).FirstOrDefault();
                if(user != null && !user.Locked)
                {
                    var passwordValid = passwordHasher.AuthenticatePassword(login.Password, user.Password);
                    if((!user.Locked) && 
                        (user.PasswordExpires > DateTime.Now) && 
                        (user.EmailConfirmed) && 
                        (!user.ForcePasswordReset) &&
                        (string.IsNullOrEmpty(user.ConfirmationToken)) &&
                        (passwordValid))
                        {
                            user.Authenticated = true;
                            user.LoginAttempts = 0;
                            user.LastLogin = DateTime.Now;
                            context.SaveChanges();
                            return user; 
                        }
                    else
                    {
                        if(!string.IsNullOrEmpty(login.ConfirmationToken) && passwordValid)
                        {
                            if(DateTime.Now < user.ConfirmationTokenExpires &&
                                (login.ConfirmationToken == user.ConfirmationToken))
                            {
                                user.EmailConfirmed = true;
                                user.ConfirmationToken = passwordHasher.GenerateConfirmationToken();
                                user.ConfirmationTokenExpires = DateTime.Now.AddHours(24);
                                context.SaveChanges();
                                return user;
                            }
                            else
                            {
                                user.Locked = true;
                                user.ConfirmationToken = "";
                                user.ForcePasswordReset = true;
                                user.EmailConfirmed = false;
                            }
                        }

                        user.LoginAttempts = user.LoginAttempts + 1;

                        if(user.LoginAttempts >= 5)
                        {
                            user.Locked = true;
                        }
                        context.SaveChanges();
                        if (!user.Locked)
                        {
                            return user;
                        }
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