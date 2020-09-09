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
            ReturnList();
        }
        static Type[] StaffTypes = { typeof(Staffs), typeof(AdministrativeStaff), typeof(SupportStaffs), typeof(TeachingStaffs) };
        static XmlSerializer serializer = new XmlSerializer(typeof(List<Staffs>), StaffTypes);
        List<Staffs> StaffList {get; set;}

        public void WriteData(List<Staffs>Stafflist)
        {
            StaffList = Stafflist;
            TextWriter xmlwriter = new StreamWriter(ConfigurationManager.AppSettings["Xmlfile"]);
            serializer.Serialize(xmlwriter, StaffList);
            xmlwriter.Close();
        }

        public List<Staffs> PopulateList()
        {
            return StaffList;
        }

        public void ReturnList()
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
                StaffList = (List<Staffs>)serializer.Deserialize(xmlfilestream);
                xmlfilestream.Close();
            }
            catch
            {
                StaffList= new List<Staffs>();
                xmlfilestream.Close();
            }
        }
    }
}
