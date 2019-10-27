using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TraumaRegistry.Data;
using TraumaRegistry.Data.Models;

namespace TraumaRegistry.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReferenceTableController : ControllerBase
    {
        private readonly Context _context;
        private IConfiguration _configuration;
        public ReferenceTableController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        [ActionName("GetRefTableData")]
        public ActionResult<refTableDTO> GetRefTableData(string tableName)
        {
            tableName = AdjustTableNameForProvider(tableName);
            refTableDTO table = new refTableDTO { Name = tableName };
            string sql = string.Format("SELECT * FROM {0} ", tableName);
 
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        table.tableData.Add(new refTableDTO.refTable
                        {
                            Id = Convert.ToInt32(result["Id"]),
                            Code = result["Code"].ToString(),
                            Description = result["Description"].ToString()
                        });
                    }
                }
            }
            return table;
        }


        [HttpGet]
        [ActionName("GetRefTableList")]
        public ActionResult<List<ReferenceTables>> GetRefTableList()
        {
            try
            {
                return _context.ReferenceTables.ToList();
            }
            catch (Exception)
            {

                return null;
            }
           
        }

        [HttpPatch]
        [ActionName("AddRefTableRecord")]
        public int AddRefTableRecord(string tableName, refTableDTO.refTable newRec)
        {
            tableName = AdjustTableNameForProvider(tableName);
            string sql = string.Format("INSERT INTO {0} (Code, Description) VALUES ('{1}', '{2}'); SELECT SCOPE_IDENTITY() AS Id;", tableName, newRec.Code, newRec.Description);
            int NewId = 0;
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        if(int.TryParse(result[0].ToString(), out NewId))
                        {
                            return NewId;
                        }
                    }
                }
                return NewId;
            }
        }

        [HttpPatch]
        [ActionName("EditRefTableRecord")]
        public bool EditRefTableRecord(string tableName, refTableDTO.refTable updatedRec)
        {
            try
            {
                tableName = AdjustTableNameForProvider(tableName);
                string sql = string.Format("UPDATE {0} ", tableName);
                sql += "SET Code = @Code, Description = @Description WHERE Id = @Id;";

                var Id = new SqlParameter("@Id", updatedRec.Id);
                var code = new SqlParameter("@Code", updatedRec.Code);
                var description = new SqlParameter("@Description", updatedRec.Description);

                _context.Database.ExecuteSqlRaw(sql, code, description, Id);
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

             
        }

        [HttpPatch]
        [ActionName("DeleteRefTableRecord")]
        public bool DeleteRefTableRecord(string tableName, int Id)
        {
            try
            {
                string sql = string.Format("DELETE FROM {0} ", tableName);
                sql += "WHERE Id = @Id;";

                var IdParm = new SqlParameter("@Id", Id); 

                _context.Database.ExecuteSqlRaw(sql, IdParm);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            } 
        }

        private string AdjustTableNameForProvider(string tableName)
        {
            if (_configuration.GetSection("TraumaRegistrySettings")["dbProvider"] == "postgres")
            {
                tableName = "\"" + tableName + "\"";
            }

            return tableName;
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