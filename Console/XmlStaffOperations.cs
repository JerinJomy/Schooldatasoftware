using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.Configuration;

namespace Staffs
{
    public class XmlStaffOperations : IStaffOperations
    {
        String Xmlfile = ConfigurationManager.AppSettings["Xmlfile"];
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
                List<Staffs> StaffList = (List<Staffs>)serializer.Deserialize(xmlfilestream);
                xmlfilestream.Close();
                Staffs staff = FileOperations.XMLEnterStaff();
                StaffList.Add(staff);
                TextWriter xmlwriter = new StreamWriter(Xmlfile);
                serializer.Serialize(xmlwriter, StaffList);
                xmlwriter.Close();
            }
            else
            {
                Type[] StaffTypes = { typeof(Staffs), typeof(AdministrativeStaff), typeof(SupportStaffs), typeof(TeachingStaffs) };
                XmlSerializer serializer = new XmlSerializer(typeof(List<Staffs>), StaffTypes);
                Staffs staff = FileOperations.XMLEnterStaff();
                List<Staffs> StaffList=new List<Staffs>();
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
            TextWriter xmlwriter = new StreamWriter(Xmlfile);
            serializer.Serialize(xmlwriter, FileOperations.UpdateStaff(StaffList, index));
            xmlwriter.Close();
        }

        public void View()
        {
            var xmlfilestream = new FileStream(Xmlfile, FileMode.Open);
            Type[] StaffTypes = { typeof(Staffs), typeof(AdministrativeStaff), typeof(SupportStaffs), typeof(TeachingStaffs) };
            XmlSerializer serializer = new XmlSerializer(typeof(List<Staffs>), StaffTypes);
            List<Staffs> StaffList = (List<Staffs>)serializer.Deserialize(xmlfilestream);
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

        public static int IdValue()
        {
            int largest = 0;
            String Xmlfile = ConfigurationManager.AppSettings["Xmlfile"];
            try
            {
                Type[] StaffTypes = { typeof(Staffs), typeof(AdministrativeStaff), typeof(SupportStaffs), typeof(TeachingStaffs) };
                XmlSerializer serializer = new XmlSerializer(typeof(List<Staffs>), StaffTypes);
                var xmlfilestream = new FileStream(Xmlfile, FileMode.Open);
                List<Staffs> StaffList = (List<Staffs>)serializer.Deserialize(xmlfilestream);
                xmlfilestream.Close();
                Console.WriteLine(StaffList.Count);
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
    }

}
