using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
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
    public class PatientsPagedController : ControllerBase
    {
        private readonly Context _context;

        public PatientsPagedController(Context context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients(UrlQuery urlQuery)
        {
            var pageNumber = Convert.ToInt32(urlQuery.PageNumber);
            IQueryable<Patient> query = null;

            string sql = "SELECT * FROM dbo.Patients";

            if (!string.IsNullOrEmpty(urlQuery.filterColumn) && !string.IsNullOrEmpty(urlQuery.filter))
            {
                sql += string.Format(" WHERE {0} Like '{1}%'", urlQuery.filterColumn, urlQuery.filter);
            } 
            if (!string.IsNullOrEmpty(urlQuery.orderBy))
            {
                string orderby = string.Format(" Order By {0} {1}", urlQuery.orderBy, urlQuery.orderByDirection);
                sql += orderby;

                sql += string.Format(" OFFSET {0} * ({1} - 1) ROWS FETCH NEXT {0} ROWS ONLY OPTION(RECOMPILE); ", urlQuery.PageSize, urlQuery.PageNumber);
            }


            query = _context.Patients.FromSqlRaw(sql); 
 
            return await query.ToListAsync();
        }
    } 
    public class UrlQuery
    {
        private const int maxPageSize = 100;
        public bool IncludeCount { get; set; } = false;
        public int? PageNumber { get; set; }

        private int _pageSize = 50;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value < maxPageSize) ? value : maxPageSize;
            }
        }

        public string filterColumn { get; set; }
        public string filter { get; set; }
        public string orderBy { get; set; }
        public string orderByDirection { get; set; } = "ascending";
 
    }


    public class Pagination
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? TotalRecords { get; set; }
        public int? TotalPages => TotalRecords.HasValue ? (int)Math.Ceiling(TotalRecords.Value / (double)PageSize) : (int?)null;
    }

}