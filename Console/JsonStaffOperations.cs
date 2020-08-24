using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
namespace Staffs
{
    class JsonStaffOperations : IStaffOperations
    {
        string filepath =@"C:\Users\jerin\Documents\rckr\Firstproject\staff.json";
        string tempfile = "temp.json";
        public void EnterData()
        {
            StaffOperations.EnterData();
            List<Staffs> StaffList = StaffOperations.ReturnList();
            Staffs staff = StaffList[StaffList.Count-1];
            string jsonstring = "";
            switch (staff.StaffType)
            {
                case StaffType.TEACHINGSTAFF:
                    TeachingStaffs teachingstaff = ((TeachingStaffs)staff);
                    jsonstring = JsonConvert.SerializeObject(teachingstaff);
                    break;
                case StaffType.ADMINISTRATIVESTAFF:
                    AdministrativeStaff adminstaff = ((AdministrativeStaff)staff);
                    jsonstring = JsonConvert.SerializeObject(adminstaff);
                    break;
                case StaffType.SUPPORTSTAFF:
                    SupportStaffs supportstaffs = ((SupportStaffs)staff);
                    jsonstring = JsonConvert.SerializeObject(supportstaffs);
                    break;
            }
            using (StreamWriter sw = new StreamWriter(filepath, true))
            {
                sw.WriteLine(jsonstring);
            }

        }
        public void View()
        {
            Staffs staff;
            using (StreamReader sr = new StreamReader(filepath))
            {
                string jsonline;
                while ((jsonline = sr.ReadLine()) != null)
                {
                    staff = JsonConvert.DeserializeObject<Staffs>(jsonline);
                    switch (staff.StaffType)
                    {
                        case StaffType.TEACHINGSTAFF:
                            TeachingStaffs teachingstaff = JsonConvert.DeserializeObject<TeachingStaffs>(jsonline);
                            StaffOperations.Display(teachingstaff);
                            break;
                        case StaffType.ADMINISTRATIVESTAFF:
                            AdministrativeStaff adminstaff = JsonConvert.DeserializeObject<AdministrativeStaff>(jsonline);
                            StaffOperations.Display(adminstaff);
                            break;
                        case StaffType.SUPPORTSTAFF:
                            SupportStaffs supportstaffs = JsonConvert.DeserializeObject<SupportStaffs>(jsonline);
                            StaffOperations.Display(supportstaffs);
                            break;
                    }
                }
            }
        }
        public void Delete(int id)
        {
            Staffs staff;
            using (StreamReader sr = new StreamReader(filepath))
            {
                using (StreamWriter sw = new StreamWriter(tempfile))
                {
                    string jsonline;
                    while ((jsonline = sr.ReadLine()) != null)
                    {
                        staff = JsonConvert.DeserializeObject<Staffs>(jsonline);
                        if (staff.Id != id)
                        {
                            sw.WriteLine(jsonline);
                        }
                    }

                }
            }
            File.Delete(filepath);
            File.Move(tempfile, filepath);
        }


        public void Update(int id)
        {
            Staffs staff;
            using (StreamReader sr = new StreamReader(filepath))
            {
                using (StreamWriter sw = new StreamWriter(tempfile))
                {
                    string jsonline;
                    while ((jsonline = sr.ReadLine()) != null)
                    {
                        staff = JsonConvert.DeserializeObject<Staffs>(jsonline);
                        if (staff.Id == id)
                        {
                            id = staff.Id;
                            StaffType stafftype = staff.StaffType;
                            Console.WriteLine("enter the  name");
                            string name = Console.ReadLine();
                            Console.WriteLine("enter the phone no");
                            string phone = Console.ReadLine();
                            Console.WriteLine("enter the email id");
                            string email = Console.ReadLine();
                            switch (staff.StaffType)
                            {
                                case StaffType.TEACHINGSTAFF:
                                    Console.WriteLine("enter the classname");
                                    string className = Console.ReadLine();
                                    Console.WriteLine("enter the subject taught");
                                    string subject = Console.ReadLine();
                                    TeachingStaffs T_staff = new TeachingStaffs(stafftype, name, phone, email, className, subject, id);
                                    jsonline = JsonConvert.SerializeObject(T_staff);
                                    Console.WriteLine("ENTRY EDITED");
                                    break;
                                case StaffType.ADMINISTRATIVESTAFF:
                                    Console.WriteLine("enter the designation");
                                    string designation_a = Console.ReadLine();
                                    AdministrativeStaff A_staff = new AdministrativeStaff(stafftype, name, phone, email, id, designation_a);
                                    jsonline = JsonConvert.SerializeObject(A_staff);
                                    break;
                                case StaffType.SUPPORTSTAFF:
                                    Console.WriteLine("enter the designation");
                                    string designation_s = Console.ReadLine();
                                    SupportStaffs S_staff = new SupportStaffs(stafftype, name, phone, email, id, designation_s);
                                    jsonline = JsonConvert.SerializeObject(S_staff);
                                    break;
                            }
                            sw.WriteLine(jsonline);
                        }
                        else
                        {
                            sw.WriteLine(jsonline);
                        }

                    }
                }
            }
            File.Delete(filepath);
            File.Move(tempfile, filepath);
        }
        public void ViewOne(int id)
        {
            Staffs staff;
            using (StreamReader sr = new StreamReader(filepath))
            {
                string jsonline;
                while ((jsonline = sr.ReadLine()) != null)
                {
                    staff = JsonConvert.DeserializeObject<Staffs>(jsonline);
                    if (staff.Id == id)
                    {
                        switch (staff.StaffType)
                        {
                            case StaffType.TEACHINGSTAFF:
                                TeachingStaffs teachingstaff = JsonConvert.DeserializeObject<TeachingStaffs>(jsonline);
                                StaffOperations.Display(teachingstaff);
                                break;
                            case StaffType.ADMINISTRATIVESTAFF:
                                AdministrativeStaff adminstaff = JsonConvert.DeserializeObject<AdministrativeStaff>(jsonline);
                                StaffOperations.Display(adminstaff);
                                break;
                            case StaffType.SUPPORTSTAFF:
                                SupportStaffs supportstaffs = JsonConvert.DeserializeObject<SupportStaffs>(jsonline);
                                StaffOperations.Display(supportstaffs);
                                break;
                        }
                        break;
                    }
                }
            }
        }
    }
}