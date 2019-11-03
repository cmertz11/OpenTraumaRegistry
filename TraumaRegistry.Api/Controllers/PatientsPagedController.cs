using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TraumaRegistry.Data;
using TraumaRegistry.Data.Models;

namespace TraumaRegistry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsPagedController : ControllerBase
    {
        private readonly Context _context;
        private IConfiguration _configuration;

        public PatientsPagedController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public ActionResult<pagedData> GetPatients(UrlQuery urlQuery)
        {
            string tableName = "Patients";
            tableName = AdjustDBEntityNameForProvider(tableName);
            pagedData data = new pagedData();
            var pageNumber = Convert.ToInt32(urlQuery.PageNumber);


            string sql = string.Format("SELECT * FROM {0}", tableName);

            if (!string.IsNullOrEmpty(urlQuery.filterColumn) && !string.IsNullOrEmpty(urlQuery.filter))
            {
                urlQuery.filterColumn = AdjustDBEntityNameForProvider(urlQuery.filterColumn); 
                sql += string.Format(" WHERE {0} Like '{1}%'", urlQuery.filterColumn, urlQuery.filter);
            }
            data.recordCount = _context.Patients.Count();
           
            if (!string.IsNullOrEmpty(urlQuery.orderBy))
            {
                urlQuery.orderBy = AdjustDBEntityNameForProvider(urlQuery.orderBy);
                string orderby = string.Format(" Order By {0} {1}", urlQuery.orderBy, urlQuery.orderByDirection);
                sql += orderby;
                var offset = urlQuery.PageSize * (urlQuery.PageNumber - 1);
                //sql += string.Format(" OFFSET {0} ROWS FETCH NEXT {0} ROWS ONLY OPTION(RECOMPILE); ", offset);
                sql += string.Format(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY; ", offset, urlQuery.PageSize);

            }

            data.records = _context.Patients.FromSqlRaw(sql).ToList();


            return data;
        }
        private string AdjustDBEntityNameForProvider(string entityName)
    {
        if (_configuration.GetSection("TraumaRegistrySettings")["dbProvider"] == "postgresql")
        {
            entityName = "\"" + entityName + "\"";
        }

        return entityName;
    }

    } 
    public class pagedData
    {
        public IEnumerable<Patient> records { get; set; }
        public int recordCount { get; set; }
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