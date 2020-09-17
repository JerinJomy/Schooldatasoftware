using System;
using System.Collections.Generic;
using System.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualBasic.CompilerServices;
using Staffs;

namespace StaffsWebAPI.Controllers
{
    //[RoutePrefix("api/[controller]")]
    [Route("api/[controller]")]
    public class StaffsController : ControllerBase
    {
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            IStaffOperations staffdb = new StaffDB();
            List<Staffs.Staffs> StaffList = staffdb.PopulateList();
            Staffs.Staffs staff = StaffOperations.GetStaffs(id, StaffList);
            if(staff==null)
            {
                return NotFound();
            }
            else
            {
                return Ok(staff);
            }  
        }
        [HttpGet]
        public IActionResult Get(String type)
        {
            IStaffOperations staffdb = new StaffDB();
            List<Staffs.Staffs> StaffList = staffdb.PopulateList();
            List<Staffs.Staffs> StaffTypeList = StaffOperations.ReturnStaffTypeList(type, StaffList);
            if(StaffTypeList==null)
            {
                return NotFound();
            }
            else
            {
                return Ok(StaffTypeList);
            }
        }
    }
}
