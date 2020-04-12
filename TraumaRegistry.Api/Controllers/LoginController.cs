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
        SecurityHelper security;

        public LoginController(IConfiguration _config, Context _context)
        {
            config = _config;
            context = _context;
            security = new SecurityHelper(config);
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
            
            if (user != null && user.Authenticated)
            {
                user.jsonToken = GenerateJSONWebToken(user);
                user.Password = "";
                return user;
            }
            else if(user != null && user.ForcePasswordReset)
            {
                user.ConfirmationToken = security.GenerateConfirmationToken();
                user.ConfirmationTokenExpires = DateTime.Now.AddMinutes(security.ConfirmationTokenExpiresMinutes());
                context.SaveChanges();
                user.Password = "";
                return user;
            }
            else
            {
                return Unauthorized();
            }
        }
        [ActionName("ResetPassword")] //<-- Probably needs all parms
        [HttpGet("{confirmationToken}")]
        public ActionResult<User> ResetPassword(string email, string confirmationToken, string currentPassword, string newPassword)
        {

            if (string.IsNullOrEmpty(confirmationToken))
                return Unauthorized();
             
            User user = context.Users.Where(u => u.EmailAddress == email).FirstOrDefault();


            if (user != null &&
                !user.Locked &&
                user.ConfirmationToken == confirmationToken &&
                user.ConfirmationTokenExpires > DateTime.Now &&
                security.AuthenticatePassword(currentPassword, user.Password))
            {
                user.Authenticated = false;
                if (security.ValididatePasswordFormat(newPassword))
                {
                    user.Password = security.Hash(newPassword);
                    user.jsonToken = GenerateJSONWebToken(user);
                    user.PasswordExpires = DateTime.Now.AddDays(security.PasswordExpiresDays()); //TODO: make this a setting.
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

        // POST: api/Patients
        [HttpPost]
        public ActionResult<EmailConfirmationResponse> PostConfirmEmail(string token)
        {
            try
            {
                EmailConfirmationResponse confirmation = new EmailConfirmationResponse();
                var decryptedToken = security.DecryptTokenObject(token); 

                var user = context.Users.Where(u => u.EmailAddress == decryptedToken.EmailAddress).FirstOrDefault();

                if(user.ConfirmationToken != decryptedToken.Token)
                {
                    confirmation.Messages.Add("Unauthorized");
                    confirmation.Success = false;

                    return confirmation;
                }

                if (user.ConfirmationToken == decryptedToken.Token && DateTime.Now < user.ConfirmationTokenExpires)
                {
                    user.EmailConfirmed = true;
                    user.ConfirmationToken = security.GenerateConfirmationToken();
                    user.ConfirmationTokenExpires = DateTime.Now.AddMinutes(security.ConfirmationTokenExpiresMinutes()); 
                    context.SaveChanges();

                    confirmation.Token = security.EncryptTokenObject(user.EmailAddress, user.ConfirmationToken);
                    confirmation.Success = true;
                    confirmation.Messages.Add("Email Confirmed");
                    return confirmation;
                }
                else
                {
                    confirmation.Token = "";
                    confirmation.Success = false;
                    confirmation.Messages.Add("Link Expired.  Please contact the system Administrator.");
                    return confirmation;
                }
               
            }
            catch (Exception ex)
            { 
                throw;
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
                    var passwordValid = security.AuthenticatePassword(login.Password, user.Password);
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
                        if(user.ForcePasswordReset && passwordValid)
                        {
                            if(DateTime.Now < user.ConfirmationTokenExpires)
                            {
                                user.ConfirmationToken = security.GenerateConfirmationToken();
                                user.ConfirmationTokenExpires = DateTime.Now.AddMinutes(security.ConfirmationTokenExpiresMinutes());
                                context.SaveChanges();
                                return user;
                            }
                            else
                            {
                                user.Locked = true;
                                user.ConfirmationToken = "";
                                user.ForcePasswordReset = true;
                                user.EmailConfirmed = false;
                                return user;
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