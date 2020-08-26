using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;

namespace Staffs
{
    public class XmlStaffOperations : IStaffOperations
    {
        // XML XML = new XML();
        String Xmlfile = @"C:\Users\jerin\Documents\rckr\Firstproject\staff.xml";
        List<Staffs> StaffList = new List<Staffs>();
        public void Delete(int id)
        {
            Type[] StaffTypes = { typeof(Staffs), typeof(AdministrativeStaff), typeof(SupportStaffs), typeof(TeachingStaffs) };
            XmlSerializer serializer = new XmlSerializer(typeof(List<Staffs>), StaffTypes);
            var xmlfilestream = new FileStream(Xmlfile, FileMode.Open);
            List<Staffs> StaffList = (List<Staffs>)serializer.Deserialize(xmlfilestream);
            xmlfilestream.Close();
            int index = StaffList.FindIndex(s => (s.Id == id));
            if (index == -1)
            {
                Console.WriteLine("NO STAFF AT THIS ID");
            }

            else
            {
                StaffList.RemoveAt(index);
                TextWriter xmlwriter = new StreamWriter(Xmlfile);
                serializer.Serialize(xmlwriter, StaffList);
                xmlwriter.Close();
                Console.WriteLine("entry deleted");
            }

        }

        public void EnterData()
        {
            if (File.Exists(Xmlfile))
            {
                Type[] StaffTypes = { typeof(Staffs), typeof(AdministrativeStaff), typeof(SupportStaffs), typeof(TeachingStaffs) };
                XmlSerializer serializer = new XmlSerializer(typeof(List<Staffs>), StaffTypes);
                var xmlfilestream = new FileStream(Xmlfile, FileMode.Open);
                StaffList = (List<Staffs>)serializer.Deserialize(xmlfilestream);
                xmlfilestream.Close();
                Staffs staff = EnterStaff();
                StaffList.Add(staff);
                TextWriter xmlwriter = new StreamWriter(Xmlfile);
                serializer.Serialize(xmlwriter, StaffList);
                xmlwriter.Close();
            }
            else
            {
                Type[] StaffTypes = { typeof(Staffs), typeof(AdministrativeStaff), typeof(SupportStaffs), typeof(TeachingStaffs) };
                XmlSerializer serializer = new XmlSerializer(typeof(List<Staffs>), StaffTypes);
                Staffs staff = EnterStaff();
                StaffList.Add(staff);
                TextWriter xmlwriter = new StreamWriter(Xmlfile);
                serializer.Serialize(xmlwriter, StaffList);
                xmlwriter.Close();
            }
        }

        public void Update(int id)
        {
            var xmlfilestream = new FileStream(Xmlfile, FileMode.Open);
            Type[] StaffTypes = { typeof(Staffs), typeof(AdministrativeStaff), typeof(SupportStaffs), typeof(TeachingStaffs) };
            XmlSerializer serializer = new XmlSerializer(typeof(List<Staffs>), StaffTypes);
            List<Staffs> StaffList = (List<Staffs>)serializer.Deserialize(xmlfilestream);
            xmlfilestream.Close();
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
                TextWriter xmlwriter = new StreamWriter(Xmlfile);
                serializer.Serialize(xmlwriter, StaffList);
                xmlwriter.Close();
            }


        }

        public void View()
        {
            var xmlfilestream = new FileStream(Xmlfile, FileMode.Open);
            Type[] StaffTypes = { typeof(Staffs), typeof(AdministrativeStaff), typeof(SupportStaffs), typeof(TeachingStaffs) };
            XmlSerializer serializer = new XmlSerializer(typeof(List<Staffs>), StaffTypes);
            StaffList = (List<Staffs>)serializer.Deserialize(xmlfilestream);
            xmlfilestream.Close();
            foreach (Staffs staff in StaffList)
            {
                StaffOperations.Display(staff);
            }
        }

        public void ViewOne(int id)
        {
            var xmlfilestream = new FileStream(Xmlfile, FileMode.Open);
            Type[] StaffTypes = { typeof(Staffs), typeof(AdministrativeStaff), typeof(SupportStaffs), typeof(TeachingStaffs) };
            XmlSerializer serializer = new XmlSerializer(typeof(List<Staffs>), StaffTypes);
            List<Staffs> StaffList = (List<Staffs>)serializer.Deserialize(xmlfilestream);
            xmlfilestream.Close();
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
            String Xmlfile = @"C:\Users\jerin\Documents\rckr\Firstproject\staff.xml";
            try
            {
                Type[] StaffTypes = { typeof(Staffs), typeof(AdministrativeStaff), typeof(SupportStaffs), typeof(TeachingStaffs) };
                XmlSerializer serializer = new XmlSerializer(typeof(List<Staffs>), StaffTypes);
                var xmlfilestream = new FileStream(Xmlfile, FileMode.Open);
                List<Staffs> StaffList = (List<Staffs>)serializer.Deserialize(xmlfilestream);
                xmlfilestream.Close();
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
            }  while (select != "9"); 
        } 
    }

}
