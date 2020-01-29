using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenTraumaRegistry.Data;
using OpenTraumaRegistry.Data.Models;

namespace OpenTraumaRegistry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Context _context;
        public UsersController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // POST: api/Patients
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        { 
            try 
	        {	        
		            _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetPatient", new { id = user.Id }, user);
	        }
	        catch (Exception ex)
	        {
                return BadRequest("Could not add user.");
	        } 
        }


        // PUT: api/Patients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        // GET: api/Vitals/5
        [HttpGet("{emailAddress}")]
        // TODO: _User should be changed to _dtoUser to avoid confusion.
        public async Task<ActionResult<_User>> GetUser(string emailAddress)
        {
            try
            {
                _User dtoUser = new _User();
                var user = await _context.Users.Where(u => u.EmailAddress == emailAddress).FirstOrDefaultAsync();
                dtoUser.UserId = user.Id;
                dtoUser.FirstName = user.FirstName;
                dtoUser.LastName = user.LastName;
                dtoUser.email = user.EmailAddress;
                dtoUser.SystemAdministrator = user.SystemAdministrator;
                dtoUser.ConfirmationToken = user.ConfirmationToken;
                dtoUser.ConfirmationTokenExpires = user.ConfirmationTokenExpires;
                dtoUser.PasswordExpires = user.PasswordExpires;
                dtoUser.EmailConfirmed = user.EmailConfirmed;

                var recs = await _context.UserFacilities.
                    Where(u => u.UserId == user.Id)
                    .Include(f => f.Facility)
                    .ToListAsync();
                foreach (var item in recs)
                {
                    dtoUser.userFacilities.Add(new _UserFacility { FacilityId = item.FacilityId, FacilityName = item.Facility.FacilityName, Administrator = item.Administrator });
                }
                return dtoUser;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        [HttpGet("{Token}/{Id}")]
        public async Task<ActionResult<bool>> ConfirmEmailToken(string Token, int Id)
        {
            if (string.IsNullOrEmpty(Token) || Id < 0)
            {
                return false;
            }

            var user = _context.Users.Where(u => u.Id == Id).FirstOrDefault();
            if (user.ConfirmationToken == Token && user.ConfirmationTokenExpires > DateTime.Now)
            {
                return true;
            }

            return false;
        }

        public class _User
        {
            public int UserId { get; set; }
            public string email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public bool SystemAdministrator { get; set; }
            public DateTime PasswordExpires { get; set; }
            public string Token { get; set; }
            public bool EmailConfirmed { get; set; }
            public string ConfirmationToken { get; set; }
            public DateTime ConfirmationTokenExpires { get; set; }
            public List<_UserFacility> userFacilities { get; set; } = new List<_UserFacility>();
        }
        public class _UserFacility
        {
            public int FacilityId { get; set; }
            public string FacilityName { get; set; }
            public bool Administrator { get; set; }
        }
    }

    
} 
