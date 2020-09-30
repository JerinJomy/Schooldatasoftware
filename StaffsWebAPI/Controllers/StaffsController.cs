using System;
using System.Collections.Generic;
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
        public StaffsController()
        {
            StaffList = staffdb.StaffList;
        }
        public StaffDB staffdb = new StaffDB();
        public List<Staffs.Staffs> StaffList { get; set; }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            Staffs.Staffs staff = StaffApiHelper.GetStaffs(id, StaffList);
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
            List<Staffs.Staffs> StaffTypeList = new List<Staffs.Staffs>();
            switch (type)
            {
                case "teaching":
                    StaffTypeList = StaffList.FindAll(x => x.StaffType == StaffType.TEACHINGSTAFF);
                    List<TeachingStaffs> TeachingStaffsList = StaffTypeList.Cast<TeachingStaffs>().ToList();
                    return Ok(TeachingStaffsList);
                case "administrative":
                    StaffTypeList = StaffList.FindAll(x => x.StaffType == StaffType.ADMINISTRATIVESTAFF);
                    List<AdministrativeStaff> AdministrativeStaffsList = StaffTypeList.Cast<AdministrativeStaff>().ToList();
                    return Ok(AdministrativeStaffsList);
                case "support":
                    StaffTypeList = StaffList.FindAll(x => x.StaffType == StaffType.SUPPORTSTAFF);
                    List<SupportStaffs> SupportStaffsList = StaffTypeList.Cast<SupportStaffs>().ToList();
                    return Ok(SupportStaffsList);
                default:
                    return NotFound();
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] object json)
        {
            StaffList.Add(StaffApiHelper.InsertStaff(json, StaffList));
            staffdb.WriteData(StaffList);
            return Ok(StaffList[StaffList.Count - 1]);
        }

        [HttpPut]
        [Route("{id:int}")]            
        public IActionResult Put(int id,[FromBody] object json)
        {
            Staffs.Staffs staff = JsonConvert.DeserializeObject<Staffs.Staffs>(json.ToString());
            if(staff.Id!=id)
            {
                return BadRequest();
            }
            int index = StaffList.FindIndex(s => (s.Id == id));
            //StaffList.Exists
            if(index==-1)
            {
                return NotFound();
            }
            else
            {

                switch (StaffList[index].StaffType)
                {
                    case StaffType.TEACHINGSTAFF:
                        TeachingStaffs staff_t=JsonConvert.DeserializeObject<TeachingStaffs>(json.ToString());
                        ((TeachingStaffs)StaffList[index]).UpdateTeaching(staff_t.Name, staff_t.Phone, staff_t.Email, staff_t.ClassName, staff_t.Subject);
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
            StaffList = staffdb.StaffList;
            int index = StaffList.FindIndex(s => (s.Id == id));
            if (index == -1)
            {
                return NotFound();
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
