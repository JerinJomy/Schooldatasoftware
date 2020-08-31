using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Configuration;
namespace Staffs
{
    public class JsonStaffOperations : IStaffOperations
    {
        static private readonly JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
        List<Staffs> StaffList = ReturnList();
        public void EnterData()
        {
            StaffList.Add(StaffOperations.EnterData(StaffList));
        }
        public void View()
        {
            StaffOperations.View(StaffList);
        }
        public void Delete(int id)
        {
            StaffOperations.Delete(id, StaffList);
        }
        public void Update(int id)
        {
            StaffOperations.UpdateData(id, StaffList);
        }
        public void ViewOne(int id)
        {
            StaffOperations.ViewOne(id, StaffList);
        }
        public void Deserialize()
        {
            string jsonline = JsonConvert.SerializeObject(StaffList.ToArray(), Formatting.Indented, settings);
            File.WriteAllText(ConfigurationManager.AppSettings["Jsonfile"], string.Empty);
            File.WriteAllText(ConfigurationManager.AppSettings["Jsonfile"], jsonline);
        }

        public static List<Staffs> ReturnList()
        {
            if (!File.Exists(ConfigurationManager.AppSettings["Jsonfile"]))
            {
                TextWriter tw = new StreamWriter(ConfigurationManager.AppSettings["xmlfile"]);
                tw.Close();
            }
            try
            {
                string Jsonstring = File.ReadAllText(ConfigurationManager.AppSettings["Jsonfile"]);
                List<Staffs> StaffList = JsonConvert.DeserializeObject<List<Staffs>>(Jsonstring, settings);
                return StaffList;
            }
            catch
            {
                List<Staffs> StaffList= new List<Staffs>();
                return StaffList;
            }
        }
    }
}