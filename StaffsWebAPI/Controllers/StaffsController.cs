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
using Staffs;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

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
            StaffDB staffdb = new StaffDB();
            List<Staffs.Staffs> StaffList = staffdb.StaffList;
            Staffs.Staffs staff = ApiOperations.GetStaffs(id, StaffList);
            if (staff == null)
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
            StaffDB staffdb = new StaffDB();
            List<Staffs.Staffs> StaffList = staffdb.StaffList;
            List<Staffs.Staffs> StaffTypeList = ApiOperations.ReturnStaffTypeList(type, StaffList);
            if (StaffTypeList == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(StaffTypeList);
            }

        }
        [HttpPost]
        public IActionResult Post([FromBody] object json)
        {
            StaffDB staffdb = new StaffDB();
            List<Staffs.Staffs> StaffList = staffdb.StaffList;
            StaffList = ApiOperations.InsertStaff(json, StaffList);
            staffdb.WriteData(StaffList);
            return Ok(StaffList[StaffList.Count - 1]);
        }

        [HttpPut]
        [Route("{id:int}")]            
        public IActionResult Put(int id,[FromBody] object json)
        {
            StaffDB staffdb = new StaffDB();
            List<Staffs.Staffs> StaffList = staffdb.StaffList;
            int index = StaffList.FindIndex(s => (s.Id == id));
            if(index==-1)
            {
                return NoContent();
            }
            else
            {

                switch (StaffList[index].StaffType)
                {
                    case StaffType.TEACHINGSTAFF:
                        TeachingStaffs staff=JsonConvert.DeserializeObject<TeachingStaffs>(json.ToString());
                        ((TeachingStaffs)StaffList[index]).UpdateTeaching(staff.Name, staff.Phone, staff.Email, staff.ClassName, staff.Subject);
                        break;
                    case StaffType.ADMINISTRATIVESTAFF:
                        AdministrativeStaff staff_a = JsonConvert.DeserializeObject<AdministrativeStaff>(json.ToString());
                        ((AdministrativeStaff)StaffList[index]).UpdateAdministrative(staff_a.Name, staff_a.Phone, staff_a.Email,staff_a.Designation);
                        break;
                    case StaffType.SUPPORTSTAFF:
                        SupportStaffs staff_s = JsonConvert.DeserializeObject<SupportStaffs>(json.ToString());
                        ((SupportStaffs)StaffList[index]).UpdateSupport(staff_s.Name, staff_s.Phone, staff_s.Email, staff_s.Designation);
                        break;
                }
                staffdb.WriteData(StaffList);
                return Ok(StaffList[index]);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            StaffDB staffdb = new StaffDB();
            List<Staffs.Staffs> StaffList = staffdb.StaffList;
            int index = StaffList.FindIndex(s => (s.Id == id));
            if (index == -1)
            {
                return NoContent();
            }
            else
            {
                StaffList.RemoveAt(index);
                staffdb.WriteData(StaffList);
                return Ok();
            }
        }

    }
}
