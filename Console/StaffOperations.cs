using System;
using System.Collections.Generic;
using System.Linq;


namespace Staffs
{
    public static class StaffOperations
    {

        private static int IdValue(List<Staffs> StaffList)
        {
            int largest = 0;
            if (StaffList.Count == 0)
            {
                return 1;
            }
            else
            {
                largest = StaffList.Max(x => x.Id);
                return largest + 1;
            }

        }
        public static Staffs EnterData(List<Staffs> StaffList)
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
                int id = StaffOperations.IdValue(StaffList);
                Staffs Staff = new TeachingStaffs(stafftype, name, phone, email, classname, subject, id);
                return Staff;
            }
            else if (stafftype == StaffType.ADMINISTRATIVESTAFF)
            {
                Console.WriteLine("Enter the designation of the staff");
                string designation = Console.ReadLine();
                int id = StaffOperations.IdValue(StaffList);
                Staffs staff = new AdministrativeStaff(stafftype, name, phone, email, id, designation);
                return staff;
            }
            else if (stafftype == StaffType.SUPPORTSTAFF)
            {
                Console.WriteLine("Enter the designation of the staff");
                string designation = Console.ReadLine();
                int id = StaffOperations.IdValue(StaffList);
                Staffs staff = new SupportStaffs(stafftype, name, phone, email, id, designation);
                return staff;
            }
            else
            {
                return null;
            }
        }

        public static void View(List<Staffs> StaffList)
        {
            if (StaffList.Count < 1)
            {
                Console.WriteLine("NO DATA ENTERED");
            }

            else
            {
                foreach (Staffs staff in StaffList)
                {
                    Display(staff);
                }

            }
        }

        public static void ViewOne(int viewid,List<Staffs> StaffList)
        {
            Staffs staff = StaffList.FirstOrDefault(x => x.Id == viewid);
            if (staff == null)
            {
                Console.WriteLine("NO STAFF WITH THIS ID");
            }
            else
            {

                Display(staff);
            }
        }

        public static void UpdateData(int updateid,List<Staffs> StaffList)
        {
            int index = StaffList.FindIndex(s => (s.Id == updateid));
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
            }
        }

        public static void Delete(int delteid, List<Staffs> StaffList)
        {
            int index = StaffList.FindIndex(s => (s.Id == delteid));
            if (index == -1)
            {
                Console.WriteLine("NO STAFF AT THIS ID");
            }

            else
            {
                StaffList.RemoveAt(index);
                Console.WriteLine("entry deleted");
            }
        }
        public static void Display(Staffs staff)
        {
            switch (staff.StaffType)
            {
                case StaffType.TEACHINGSTAFF:

                    Console.WriteLine("\nSTAFFID:{0}", staff.Id);
                    Console.WriteLine("Staff :{0}", staff.StaffType);
                    Console.WriteLine("Name :{0}", staff.Name);
                    Console.WriteLine("Phone no :{0}", staff.Phone);
                    Console.WriteLine("Email id :{0}", staff.Email);
                    Console.WriteLine("class name:{0}", ((TeachingStaffs)staff).ClassName);
                    Console.WriteLine("Subject:{0}", ((TeachingStaffs)staff).Subject);
                    break;
                case StaffType.ADMINISTRATIVESTAFF:
                    Console.WriteLine("\nSTAFFID:{0}", staff.Id);
                    Console.WriteLine("Staff :{0}", staff.StaffType);
                    Console.WriteLine("Name :{0}", staff.Name);
                    Console.WriteLine("Phone no :{0}", staff.Phone);
                    Console.WriteLine("Email id :{0}", staff.Email);
                    Console.WriteLine("Designation:{0}", ((AdministrativeStaff)staff).Designation);
                    break;
                case StaffType.SUPPORTSTAFF:
                    Console.WriteLine("\nSTAFFID:{0}", staff.Id);
                    Console.WriteLine("Staff :{0}", staff.StaffType);
                    Console.WriteLine("Name :{0}", staff.Name);
                    Console.WriteLine("Phone no :{0}", staff.Phone);
                    Console.WriteLine("Email id :{0}", staff.Email);
                    Console.WriteLine("Designation:{0}", ((SupportStaffs)staff).Designation);
                    break;
            }
        }
        public static int ReturnId()
        {
            Console.WriteLine("ENTER THE STAFF id");
            try
            {
                int id = Convert.ToInt32(Console.ReadLine());
                return id;
            }
            catch
            {
                return -1;
            }
        }
    }
}



