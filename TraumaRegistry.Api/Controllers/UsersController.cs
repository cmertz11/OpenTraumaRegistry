using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenTraumaRegistry.Data;
using OpenTraumaRegistry.Data.Models;
using static OpenTraumaRegistry.Api.Models.UserModels;

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
        public async Task<ActionResult<IEnumerable<_dtoUser>>> GetUsers()
        {
            List<_dtoUser> dtoUsers = new List<_dtoUser>();
            var userList = await _context.Users.ToListAsync();
            foreach (var item in userList)
            {
                dtoUsers.Add(TranslateUserTo_dtoUser(item));
            }
            return dtoUsers;
        }

        [HttpPost]
        public async Task<ActionResult<_dtoUser>> PostUser(_dtoUser _dtoUser)
        { 
            try 
	        {	        
		        _context.Users.Add(Translate_dtoUserToUser(_dtoUser));
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetUser", new { id = _dtoUser.Id }, _dtoUser);
	        }
	        catch (Exception ex)
	        {
                return BadRequest("Could not add user.");
	        } 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, _dtoUser user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            
            _context.Entry(Translate_dtoUserToUser(user)).State = EntityState.Modified;

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

        [ActionName("GetUserByEmailAddress")]
        [HttpGet("{emailAddress}")]
        public async Task<ActionResult<_dtoUser>> GetUserUserByEmailAddress(string emailAddress)
        {
            try
            {
                _dtoUser dtoUser = new _dtoUser();
                var user = await _context.Users.Where(u => u.EmailAddress == emailAddress).FirstOrDefaultAsync();
                dtoUser = TranslateUserTo_dtoUser(user);

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

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private _dtoUser TranslateUserTo_dtoUser(User user)
        {
            _dtoUser dtoUser = new _dtoUser();
            dtoUser.Id = user.Id;
            dtoUser.FirstName = user.FirstName;
            dtoUser.LastName = user.LastName;
            dtoUser.OfficePhone = user.OfficePhone;
            dtoUser.CellPhone = user.CellPhone;
            dtoUser.EmailAddress = user.EmailAddress;
            dtoUser.SystemAdministrator = user.SystemAdministrator;
            dtoUser.ConfirmationToken = user.ConfirmationToken;
            dtoUser.ConfirmationTokenExpires = user.ConfirmationTokenExpires;
            dtoUser.PasswordExpires = user.PasswordExpires;
            dtoUser.EmailConfirmed = user.EmailConfirmed;
            return dtoUser;
        }

        private User Translate_dtoUserToUser(_dtoUser _dtoUser)
        {
            User user = new User();
            user.Id = _dtoUser.Id;
            user.FirstName = _dtoUser.FirstName;
            user.LastName = _dtoUser.LastName;
            user.EmailAddress = _dtoUser.EmailAddress;
            user.OfficePhone = _dtoUser.OfficePhone;
            user.CellPhone = _dtoUser.CellPhone;
            user.SystemAdministrator = _dtoUser.SystemAdministrator;
            user.ConfirmationToken = _dtoUser.ConfirmationToken;
            user.ConfirmationTokenExpires = _dtoUser.ConfirmationTokenExpires;
            user.PasswordExpires = _dtoUser.PasswordExpires;
            user.EmailConfirmed = _dtoUser.EmailConfirmed;
            return user;
        }


    }

    
} 
