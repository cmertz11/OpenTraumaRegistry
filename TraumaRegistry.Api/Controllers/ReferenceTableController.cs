using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OpenTraumaRegistry.Data;
using OpenTraumaRegistry.Data.Models;

namespace OpenTraumaRegistry.Api.Controllers
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
            refTableDTO table = new refTableDTO { Name = tableName };

            Type targetType = FindType(tableName);
            var method = typeof(DbContext).GetMethod("Set").MakeGenericMethod(targetType);
            var query = method.Invoke(_context, null) as IQueryable;

            foreach (var item in query)
            {
                table.tableData.Add(
                    new refTableDTO.refTable { 
                        Id = (int)GetPropValue(item, "Id"), 
                        Code = (string)GetPropValue(item, "Code"), 
                        Description = (string)GetPropValue(item, "Description") });
            }

            return table;
        }

        [HttpPatch]
        [ActionName("AddRefTableRecord")]
        public bool AddRefTableRecord(string tableName, refTableDTO.refTable newRec)
        {
            try
            {
                Type targetType = FindType(tableName);
                var refTableObj = Activator.CreateInstance(targetType);
                refTableObj = SetPropValue(refTableObj, "Code", newRec.Code);
                refTableObj = SetPropValue(refTableObj, "Description", newRec.Description);
            
                _context.AddRange(refTableObj);  
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            } 
        }

        [HttpPatch]
        [ActionName("EditRefTableRecord")]
        public bool EditRefTableRecord(string tableName, refTableDTO.refTable updatedRec)
        {
            try
            {
                Type targetType = FindType(tableName);
                var refTableObj = Activator.CreateInstance(targetType);
                refTableObj = SetPropValue(refTableObj, "Id", updatedRec.Id);
                refTableObj = SetPropValue(refTableObj, "Code", updatedRec.Code);
                refTableObj = SetPropValue(refTableObj, "Description", updatedRec.Description);
                _context.Update(refTableObj);
                _context.SaveChanges();
                
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
                Type targetType = FindType(tableName);
                var refTableObj = Activator.CreateInstance(targetType);
                refTableObj = SetPropValue(refTableObj, "Id", Id);
                _context.Remove(refTableObj);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            } 
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

        private static Type FindType(string name)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var result = (from elem in (from app in assemblies
                                        select (from tip in app.GetTypes()
                                                where tip.Name == name.Trim()
                                                select tip).FirstOrDefault())
                          where elem != null
                          select elem).FirstOrDefault();

            return result;
        }

        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        private static object SetPropValue(object src, string propName, object valObj)
        {
            object boxedObject = RuntimeHelpers.GetObjectValue(src);
            src.GetType().GetProperty(propName).SetValue(boxedObject, valObj);
            return src;
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
                public bool Selected { get; set; } = false;

            }
        }

    }
}