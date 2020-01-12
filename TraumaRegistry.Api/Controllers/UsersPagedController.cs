using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenTraumaRegistry.Data;
using OpenTraumaRegistry.Data.Models;
using static OpenTraumaRegistry.Api.Models.Paging;

namespace OpenTraumaRegistry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersPagedController : ControllerBase
    {
        private readonly Context _context;
        private IConfiguration _configuration;
        private string _databasename;
        public UsersPagedController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public ActionResult<pagedUserData> GetUsers(UrlQuery urlQuery)
        {
            string tableName = "Users";
            _databasename = _context.Database.GetDbConnection().Database;
            tableName = AdjustDBEntityNameForProvider(tableName);
            pagedUserData data = new pagedUserData();
            var pageNumber = Convert.ToInt32(urlQuery.PageNumber);
            data.recordCount = _context.Patients.Count();
            string sql = GenerateSql(urlQuery, tableName, data); 
            data.records = _context.Users.FromSqlRaw(sql).ToList();
            return data;
        }

        private string GenerateSql(UrlQuery urlQuery, string tableName, pagedUserData data)
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
    public class pagedUserData
    {
        public IEnumerable<User> records { get; set; }
        public int recordCount { get; set; }

        public int eventCount { get; set; }
    }



}