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
using OpenTraumaRegistry.Shared;

namespace OpenTraumaRegistry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Context _context;
        private readonly ObjectMapper _objectMapper = new ObjectMapper();
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
                dtoUsers.Add((_dtoUser)_objectMapper.MapObjects(item, new _dtoUser()));
            }
            return dtoUsers;
        }

        [HttpPost]
        public async Task<ActionResult<_dtoUser>> PostUser(_dtoUser _dtoUser)
        { 
            try 
	        {
                User user = new User();
                user = (User)_objectMapper.MapObjects(_dtoUser, user);
		        _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetUser", new { id = _dtoUser.Id }, _dtoUser);
	        }
	        catch (Exception ex)
	        {
                return BadRequest("Could not add user.");
	        } 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, _dtoUser dtoUser)
        {
            if (id != dtoUser.Id)
            {
                return BadRequest();
            }
            var currentUserRecord = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            currentUserRecord = (User)_objectMapper.MapObjects(dtoUser, currentUserRecord);

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
                dtoUser = (_dtoUser)_objectMapper.MapObjects(user, new _dtoUser());

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

    }

    
} 
