﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Staffs;

namespace StaffsWebAPI.Controllers
{
    public static class StaffOperations
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
        public static List<Staffs.Staffs> ReturnStaffTypeList(string type,List<Staffs.Staffs> StaffList)
        {
            List<Staffs.Staffs> StaffTypeList = new List<Staffs.Staffs>();
            switch (type)
            {
                case "teaching":
                    StaffTypeList = StaffList.FindAll(x => x.StaffType == StaffType.TEACHINGSTAFF);
                    return StaffTypeList;
                case "administrative":
                    StaffTypeList = StaffList.FindAll(x => x.StaffType == StaffType.ADMINISTRATIVESTAFF);
                    return StaffTypeList;
                case "support":
                    StaffTypeList = StaffList.FindAll(x => x.StaffType == StaffType.SUPPORTSTAFF);
                    return StaffTypeList;
                default:
                    return null;
            }
        }
    }
}