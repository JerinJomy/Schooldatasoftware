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
        public XmlStaffOperations()
        {
        }
        static Type[] StaffTypes = { typeof(Staffs), typeof(AdministrativeStaff), typeof(SupportStaffs), typeof(TeachingStaffs) };
        static XmlSerializer serializer = new XmlSerializer(typeof(List<Staffs>), StaffTypes);
         List<Staffs> StaffList = ReturnList();

        public void Delete(int id)
        {
            StaffOperations.Delete(id, StaffList);
        }

        public void Deserialize()
        {
            TextWriter xmlwriter = new StreamWriter(ConfigurationManager.AppSettings["Xmlfile"]);
            serializer.Serialize(xmlwriter, StaffList);
            xmlwriter.Close();
        }

        public void EnterData()
        {
            StaffList.Add(StaffOperations.EnterData(StaffList));
        }

        public void Update(int id)
        {
            StaffOperations.UpdateData(id, StaffList);
        }

        public void View()
        {
            StaffOperations.View(StaffList);
        }

        public void ViewOne(int id)
        {
            StaffOperations.ViewOne(id, StaffList);
        }
        public static List<Staffs> ReturnList()
        {
            if (!File.Exists(ConfigurationManager.AppSettings["xmlfile"]))
            {
                TextWriter tw = new StreamWriter(ConfigurationManager.AppSettings["xmlfile"]);
                tw.Close();
            }
            Type[] StaffTypes = { typeof(Staffs), typeof(AdministrativeStaff), typeof(SupportStaffs), typeof(TeachingStaffs) };
            XmlSerializer serializer = new XmlSerializer(typeof(List<Staffs>), StaffTypes);
            FileStream xmlfilestream = new FileStream(ConfigurationManager.AppSettings["Xmlfile"], FileMode.Open);
            try
            {
                List<Staffs> StaffList = (List<Staffs>)serializer.Deserialize(xmlfilestream);
                xmlfilestream.Close();
                return StaffList;
            }
            catch
            {
                List<Staffs> StaffList= new List<Staffs>();
                xmlfilestream.Close();
                return StaffList;
            }
        }
    }
}
