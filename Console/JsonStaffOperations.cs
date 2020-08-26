using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
namespace Staffs
{
    public class JsonStaffOperations : IStaffOperations
    {
        string filepath = @"C:\Users\jerin\Documents\rckr\Firstproject\staff.json";
        private readonly JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
        string tempfile = "temp.json";
        public void EnterData()
        {
            if (File.Exists(filepath))
            {
                string Jsonstring = File.ReadAllText(filepath);
                List<Staffs> StaffList = JsonConvert.DeserializeObject<List<Staffs>>(Jsonstring, settings);
                Staffs staff = EnterStaff();
                StaffList.Add(staff);
                string jsonline = JsonConvert.SerializeObject(StaffList.ToArray(), Formatting.Indented, settings);
                File.WriteAllText(filepath, string.Empty);
                File.WriteAllText(filepath, jsonline);
            }
            else
            {
                List<Staffs> StaffList = new List<Staffs>();
                Staffs staff = EnterStaff();
                StaffList.Add(staff);
                string jsonline = JsonConvert.SerializeObject(StaffList.ToArray(), Formatting.Indented, settings);
                File.WriteAllText(filepath, jsonline);
            }

        }
        public void View()
        {
            string Jsonstring = File.ReadAllText(filepath);
            List<Staffs> Stafflist = JsonConvert.DeserializeObject<List<Staffs>>(Jsonstring, settings);
            foreach (Staffs staff in Stafflist)
            {
                StaffOperations.Display(staff);
            }
        }
        public void Delete(int id)
        {
            string Jsonstring = File.ReadAllText(filepath);
            List<Staffs> StaffList = JsonConvert.DeserializeObject<List<Staffs>>(Jsonstring, settings);
            int index = StaffList.FindIndex(s => (s.Id == id));
            if (index == -1)
            {
                Console.WriteLine("NO STAFF AT THIS ID");
            }

            else
            {
                StaffList.RemoveAt(index);
                string jsonline = JsonConvert.SerializeObject(StaffList.ToArray(), Formatting.Indented, settings);
                File.WriteAllText(filepath, jsonline);
                Console.WriteLine("entry deleted");
            }
        }


        public void Update(int id)
        {
            string Jsonstring = File.ReadAllText(filepath);
            List<Staffs> StaffList = JsonConvert.DeserializeObject<List<Staffs>>(Jsonstring, settings);
            int index = StaffList.FindIndex(s => (s.Id == id));
            if (index == -1)
            {
                Console.WriteLine("NO STAFF AT THIS ID");
            }
            else
            {
                Console.WriteLine("enter the  name");
                string name = Console.ReadLine();
                Console.WriteLine("enter the phone no");
                string phone = Console.ReadLine();
                Console.WriteLine("enter the email id");
                string email = Console.ReadLine();
                switch (StaffList[index].StaffType)
                {
                    case StaffType.TEACHINGSTAFF:
                        Console.WriteLine("enter the classname");
                        string classname = Console.ReadLine();
                        Console.WriteLine("enter the subject taught");
                        string subject = Console.ReadLine();
                        ((TeachingStaffs)StaffList[index]).UpdateTeaching(name, phone, email, classname, subject);
                        Console.WriteLine("ENTRY EDITED");
                        break;
                    case StaffType.ADMINISTRATIVESTAFF:
                        Console.WriteLine("enter the designation");
                        string designation2 = Console.ReadLine();
                        ((AdministrativeStaff)StaffList[index]).UpdateAdministrative(name, phone, email, designation2);
                        break;
                    case StaffType.SUPPORTSTAFF:
                        Console.WriteLine("enter the designation");
                        string designation1 = Console.ReadLine();
                        ((SupportStaffs)StaffList[index]).UpdateSupport(name, phone, email, designation1);
                        break;
                }
                string jsonline = JsonConvert.SerializeObject(StaffList.ToArray(), Formatting.Indented, settings);
                File.WriteAllText(filepath, jsonline);
            }
        }
        public void ViewOne(int id)
        {
            string Jsonstring = File.ReadAllText(filepath);
            List<Staffs> StaffList = JsonConvert.DeserializeObject<List<Staffs>>(Jsonstring, settings);
            Staffs staff = StaffList.FirstOrDefault(x => x.Id == id);
            if (staff == null)
            {
                Console.WriteLine("NO STAFF WITH THIS ID");
            }
            else
            {

                StaffOperations.Display(staff);
            }
        }
        public static Staffs EnterStaff()
        {
            Console.WriteLine("enter '1' for Teaching Staff\nenter '2' for Administrative Staff\nenter '3' for Support Staff");
            string stype = Console.ReadLine();
            StaffType stafftype = (StaffType)int.Parse(stype);
            Console.WriteLine("enter the  name");
            string name = Console.ReadLine();
            Console.WriteLine("enter the phone no");
            string phone = Console.ReadLine();
            Console.WriteLine("enter the email id");
            string email = Console.ReadLine();
            string classname, subject;
            if (stafftype == StaffType.TEACHINGSTAFF)
            {
                Console.WriteLine("enter the classname");
                classname = Console.ReadLine();
                Console.WriteLine("enter the subject taught");
                subject = Console.ReadLine();
                int id = IdValue();
                Staffs staff = new TeachingStaffs(stafftype, name, phone, email, classname, subject, id);
                return staff;
            }
            else if (stafftype == StaffType.ADMINISTRATIVESTAFF)
            {
                Console.WriteLine("Enter the designation of the staff");
                string designation = Console.ReadLine();
                int id = IdValue();
                Staffs staff = new AdministrativeStaff(stafftype, name, phone, email, id, designation);
                return staff;
            }
            else if (stafftype == StaffType.SUPPORTSTAFF)
            {
                Console.WriteLine("Enter the designation of the staff");
                string designation = Console.ReadLine();
                int id = IdValue();
                Staffs staff = new SupportStaffs(stafftype, name, phone, email, id, designation);
                return staff;
            }
            else
            {
                return null;
            }
        }
        public static int IdValue()
        {
            int largest = 0;
            string filepath = @"C:\Users\jerin\Documents\rckr\Firstproject\staff.json";
            try
            {
                string Jsonstring = File.ReadAllText(filepath);
                List<Staffs> StaffList = JsonConvert.DeserializeObject<List<Staffs>>(Jsonstring);
                if (StaffList.Count == 0)
                {
                    return 1;
                }
                else
                {
                    foreach (Staffs s in StaffList)
                    {
                        if (s.Id > largest)
                        {
                            largest = s.Id;
                        }
                    }
                    return largest + 1;
                }
            }
            catch
            {
                return 1;
            }
        }
        public static void JsonProgram()
        {
            string select;
            IStaffOperations Staff = new JsonStaffOperations();
            do
            {
                Console.WriteLine("\nENTER '1' FOR DATA ENTRY\nENTER '2' TO VIEW  DETAILS OF ALL STAFF\nENTER '3' TO VIEW STAFF DETAILS IN SPECIFIC\nENTER '4' TO DELETE STAFF DETAILS\nENTER '5' TO UPDATE STAFF DETAILS \nENTER '9' TO EXIT");
                select = Console.ReadLine();
                switch (select)
                {
                    case "1":
                        Staff.EnterData();
                        break;
                    case "2":
                        Staff.View();
                        break;
                    case "3":
                        int viewid = StaffOperations.ReturnId();
                        Staff.ViewOne(viewid);
                        break;
                    case "4":
                        int deleteid = StaffOperations.ReturnId();
                        Staff.Delete(deleteid);
                        break;
                    case "5":
                        int updateid = StaffOperations.ReturnId();
                        Staff.Update(updateid);
                        break;
                    case "9":
                        Console.WriteLine("PROGRAM ENDED");
                        break;
                    default:
                        Console.WriteLine("INVALID OPTION");
                        break;
                }
            }  while (select != "9"); 
        }
    }
}