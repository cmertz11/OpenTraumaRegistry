using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenTraumaRegistry.Data;
using OpenTraumaRegistry.Data.Models;
using OpenTraumaRegistry.Shared;

namespace OpenTraumaRegistry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly Context _context;
        SecurityHelper security;
        public AccountController(Context context)
        {
            _context = context;
        }

        [HttpPut("{emailAddress}")]
        public async Task<IActionResult> ToggleUserAccountLock(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                return BadRequest();
            }
            try
            {
                var user = _context.Users.Where(u => u.EmailAddress == emailAddress).FirstOrDefault();
                if (user != null)
                {
                    user.Locked = !user.Locked;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

            return NoContent();
        }

        [HttpGet("{emailAddress}")]
        public bool ValidateEmail(string emailAddress)
        {
            try
            {
                return !_context.Users.Where(u => u.EmailAddress == emailAddress).Any();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}