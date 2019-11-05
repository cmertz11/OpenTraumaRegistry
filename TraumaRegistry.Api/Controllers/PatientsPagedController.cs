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
        private string _databasename;
        public PatientsPagedController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public ActionResult<pagedData> GetPatients(UrlQuery urlQuery)
        {
            string tableName = "Patients";
            _databasename = _context.Database.GetDbConnection().Database;
            tableName = AdjustDBEntityNameForProvider(tableName);
            pagedData data = new pagedData();
            var pageNumber = Convert.ToInt32(urlQuery.PageNumber);
            data.recordCount = _context.Patients.Count();
            string sql = GenerateSql(urlQuery, tableName, data); 
            data.records = _context.Patients.FromSqlRaw(sql).ToList();
            foreach (var item in data.records)
            {
                item.EventCount = _context.Events.Where(e => e.Patient.Id == item.Id).Count();
            }

            return data;
        }

        private string GenerateSql(UrlQuery urlQuery, string tableName, pagedData data)
        {
            var provider = _configuration.GetSection("TraumaRegistrySettings")["dbProvider"];
            switch (provider)
            {
                case "sqlserver":
                    return sqlserver(urlQuery, tableName);  
                case "postgres":
                     return postgres(urlQuery, tableName); 
                case "mysql":
                    return mysql(urlQuery, tableName);
                default:
                    throw new Exception(string.Format("DB Provider {0} not supported.", provider));
            } 
        }

        private string mysql(UrlQuery urlQuery, string tableName)
        {
            string sql = string.Format("USE {0}; SELECT * FROM {1}", _databasename, tableName);

            if (!string.IsNullOrEmpty(urlQuery.filterColumn) && !string.IsNullOrEmpty(urlQuery.filter))
            {
                sql += string.Format(" WHERE {0} Like '{1}%'", urlQuery.filterColumn, urlQuery.filter);
            } 
            if (!string.IsNullOrEmpty(urlQuery.orderBy))
            { 
                string orderby = string.Format(" Order By {0} {1}", urlQuery.orderBy, urlQuery.orderByDirection);
                sql += orderby;
                var offset = urlQuery.PageSize * (urlQuery.PageNumber - 1);
                sql += string.Format(" LIMIT {0}, {1} ", offset, urlQuery.PageSize);
            }

            return sql;
        }

        private string sqlserver(UrlQuery urlQuery, string tableName)
        {
            string sql = string.Format("SELECT * FROM {0}", tableName);

            if (!string.IsNullOrEmpty(urlQuery.filterColumn) && !string.IsNullOrEmpty(urlQuery.filter))
            {      
                sql += string.Format(" WHERE {0} Like '{1}%'", urlQuery.filterColumn, urlQuery.filter);
            }
            

            if (!string.IsNullOrEmpty(urlQuery.orderBy))
            {
                urlQuery.orderBy = AdjustDBEntityNameForProvider(urlQuery.orderBy);
                string orderby = string.Format(" Order By {0} {1}", urlQuery.orderBy, urlQuery.orderByDirection);
                sql += orderby;
                var offset = urlQuery.PageSize * (urlQuery.PageNumber - 1);
                sql += string.Format(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY OPTION(RECOMPILE); ", offset, urlQuery.PageSize);
            }

            return sql;
        }

        private string postgres(UrlQuery urlQuery, string tableName)
        {
            string sql = string.Format("SELECT * FROM {0}", tableName);

            if (!string.IsNullOrEmpty(urlQuery.filterColumn) && !string.IsNullOrEmpty(urlQuery.filter))
            {
                urlQuery.filterColumn = AdjustDBEntityNameForProvider(urlQuery.filterColumn);
                sql += string.Format(" WHERE {0} Like '{1}%'", urlQuery.filterColumn, urlQuery.filter);
            } 

            if (!string.IsNullOrEmpty(urlQuery.orderBy))
            {
                urlQuery.orderBy = AdjustDBEntityNameForProvider(urlQuery.orderBy);
                string orderby = string.Format(" Order By {0} {1}", urlQuery.orderBy, urlQuery.orderByDirection);
                sql += orderby;
                var offset = urlQuery.PageSize * (urlQuery.PageNumber - 1);
                //sql += string.Format(" OFFSET {0} ROWS FETCH NEXT {0} ROWS ONLY OPTION(RECOMPILE); ", offset);
                sql += string.Format(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY; ", offset, urlQuery.PageSize);

            }

            return sql;
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

        public int eventCount { get; set; }
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