using System;
using System.Collections.Generic;
using System.Linq;
namespace Staffs
{
    public static class FileOperations
    {
        public static Staffs JsonEnterStaff()
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
                int id = JsonStaffOperations.IdValue();
                Staffs staff = new TeachingStaffs(stafftype, name, phone, email, classname, subject, id);
                return staff;
            }
            else if (stafftype == StaffType.ADMINISTRATIVESTAFF)
            {
                Console.WriteLine("Enter the designation of the staff");
                string designation = Console.ReadLine();
                int id = JsonStaffOperations.IdValue();
                Staffs staff = new AdministrativeStaff(stafftype, name, phone, email, id, designation);
                return staff;
            }
            else if (stafftype == StaffType.SUPPORTSTAFF)
            {
                Console.WriteLine("Enter the designation of the staff");
                string designation = Console.ReadLine();
                int id = JsonStaffOperations.IdValue();
                Staffs staff = new SupportStaffs(stafftype, name, phone, email, id, designation);
                return staff;
            }
            else
            {
                return null;
            }
        }

        public static Staffs XMLEnterStaff()
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
                int id = XmlStaffOperations.IdValue();
                Staffs staff = new TeachingStaffs(stafftype, name, phone, email, classname, subject, id);
                return staff;
            }
            else if (stafftype == StaffType.ADMINISTRATIVESTAFF)
            {
                Console.WriteLine("Enter the designation of the staff");
                string designation = Console.ReadLine();
                int id = XmlStaffOperations.IdValue();
                Staffs staff = new AdministrativeStaff(stafftype, name, phone, email, id, designation);
                return staff;
            }
            else if (stafftype == StaffType.SUPPORTSTAFF)
            {
                Console.WriteLine("Enter the designation of the staff");
                string designation = Console.ReadLine();
                int id = XmlStaffOperations.IdValue();
                Staffs staff = new SupportStaffs(stafftype, name, phone, email, id, designation);
                return staff;
            }
            else
            {
                return null;
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
            } while (select != "9");
        }
        public static void XMLProgram()
        {
            string select;
            IStaffOperations Staff = new XmlStaffOperations();
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
            } while (select != "9");
        }
        public static List<Staffs> UpdateStaff(List<Staffs> StaffList, int index)
        {
            if (index == -1)
            {
                Console.WriteLine("NO STAFF AT THIS ID");
                return StaffList;
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
                        return StaffList;
                    case StaffType.ADMINISTRATIVESTAFF:
                        Console.WriteLine("enter the designation");
                        string designation2 = Console.ReadLine();
                        ((AdministrativeStaff)StaffList[index]).UpdateAdministrative(name, phone, email, designation2);
                        return StaffList;
                    case StaffType.SUPPORTSTAFF:
                        Console.WriteLine("enter the designation");
                        string designation1 = Console.ReadLine();
                        ((SupportStaffs)StaffList[index]).UpdateSupport(name, phone, email, designation1);
                        return StaffList;
                    default:
                        return StaffList;
                }
            }
        }
    }
}