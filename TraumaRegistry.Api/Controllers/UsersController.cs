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

        // GET: api/Vitals/5
        [HttpGet("{emailAddress}")]
        // TODO: _User should be changed to _dtoUser to avoid confusion.
        public async Task<ActionResult<_User>> GetUser(string emailAddress)
        {
            try
            {
                _User dtoUser = new _User();
                var user = await _context.Users.Where(u => u.EmailAddress == emailAddress).FirstOrDefaultAsync();
                dtoUser.FirstName = user.FirstName;
                dtoUser.LastName = user.LastName;
                dtoUser.email = user.EmailAddress;
                dtoUser.SystemAdministrator = user.SystemAdministrator;

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

        public class _User
        {
            public int UserId { get; set; }
            public string email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public bool SystemAdministrator { get; set; }
            public string Token { get; set; }

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
