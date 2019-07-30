using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraumaRegistry.Data;
using TraumaRegistry.Data.Models;

namespace TraumaRegistry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientRecordController : ControllerBase
    {
        private readonly Context _context;

        public PatientRecordController(Context context)
        {
            _context = context;
        }

        // GET: api/PatientsRecord/5
        [HttpGet("{id}")]
        public ActionResult<Patient> GetPatientRecord(int id)
        {
            var patientRecord = _context.Patients
                .Include(p => p.Gender)
                .Include(p => p.Race)
                .Include(events => events.Events)
                .ThenInclude(events => events.Injuries) 
                .Include(events => events.Events)
                .ThenInclude(events => events.Vitals)
                .Include(events => events.Events)
                .ThenInclude(events => events.Risks)
                .ThenInclude(risks => risks.RefRiskData)
                .FirstOrDefault(i => i.Id == id);

            if (patientRecord == null)
            {
                return NotFound();
            }

            return patientRecord;
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
    }
}
