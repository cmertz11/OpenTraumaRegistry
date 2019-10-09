﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraumaRegistry.Data;
using TraumaRegistry.Data.Models;

namespace TraumaRegistry.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReferenceTableController : ControllerBase
    {
        private readonly Context _context;

        public ReferenceTableController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [ActionName("GetRefTableData")]
        public ActionResult<refTableDTO> GetRefTableData(string tableName)
        {
            refTableDTO table = new refTableDTO { Name = tableName };
            string sql = string.Format("SELECT * FROM dbo.{0}", tableName);


            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    while( result.Read())
                    {
                        table.tableData.Add(new refTableDTO.refTable { 
                            Id = Convert.ToInt32(result["Id"]), 
                            Code = result["Code"].ToString(), 
                            Description = result["Description"].ToString() });          
                    }
                 }
            }
            return table;
        }

        [HttpGet]
        [ActionName("GetRefTableList")]
        public ActionResult<List<String>> GetRefTableList()
        {
            string sql = string.Format("SELECT name FROM Sys.Tables where name LIKE 'Ref%'");

                List<String> refTables = new List<string>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        refTables.Add(result[0].ToString());
                    }
                        
                 
                }
            }
            return refTables;
        }

        public class refTableDTO
        {
            public string Name { get; set; }
            public List<refTable> tableData = new List<refTable>();

            public class refTable
            {
                public int Id { get; set; }
                public string Code { get; set; }
                public string Description { get; set; }

            }
        }

    }
}