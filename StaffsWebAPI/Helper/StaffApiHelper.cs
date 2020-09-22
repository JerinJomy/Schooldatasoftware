using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Staffs;

namespace StaffsWebAPI.Controllers
{
    public static class StaffApiHelper
    {
        public static Staffs.Staffs GetStaffs(int id,List<Staffs.Staffs> StaffList)
        {
            Staffs.Staffs staff = StaffList.FirstOrDefault(x => x.Id == id);
            if(staff==null)
            {
                return null;
            }
            else
            {
                switch (staff.StaffType)
                {
                    case StaffType.TEACHINGSTAFF:
                        return (TeachingStaffs)staff;
                    case StaffType.ADMINISTRATIVESTAFF:
                        return (AdministrativeStaff)staff;
                    case StaffType.SUPPORTSTAFF:
                        return (SupportStaffs)staff;
                    default:
                        return null;
                }
            }
        }
        //public static List<Staffs.Staffs> ReturnStaffTypeList(string type,List<Staffs.Staffs> StaffList)
        //{
        //    List<Staffs.Staffs> StaffTypeList = new List<Staffs.Staffs>();
        //    switch (type)
        //    {
        //        case "teaching":
        //            StaffTypeList = StaffList.FindAll(x => x.StaffType == StaffType.TEACHINGSTAFF);
        //            List<TeachingStaffs> TeachingStaffsList = StaffTypeList.Cast<TeachingStaffs>().ToList();
        //            return TeachingStaffsList;
        //        case "administrative":
        //            StaffTypeList = StaffList.FindAll(x => x.StaffType == StaffType.ADMINISTRATIVESTAFF);
        //            return StaffTypeList;
        //        case "support":
        //            StaffTypeList = StaffList.FindAll(x => x.StaffType == StaffType.SUPPORTSTAFF);
        //            return StaffTypeList;
        //        default:
        //            return null;
        //    }
        //}
       public static Staffs.Staffs InsertStaff(object json, List<Staffs.Staffs> StaffList)
        {
            dynamic dynamicstaff = JsonConvert.DeserializeObject(json.ToString());
            Staffs.Staffs staff=new Staffs.Staffs();
            int typeno = (int)dynamicstaff.staffType;
            switch (typeno)
            {
                case 1:
                    staff = JsonConvert.DeserializeObject<TeachingStaffs>(json.ToString());
                    staff.Id = StaffDB.GetId();
                    return staff;
                case 2:
                    staff = JsonConvert.DeserializeObject<AdministrativeStaff>(json.ToString());
                    staff.Id = StaffDB.GetId();
                    return staff;
                case 3:
                    staff = JsonConvert.DeserializeObject<SupportStaffs>(json.ToString());
                    staff.Id = StaffDB.GetId();
                    return staff;
                default:
                    return staff;
            }
        }
    }
}
