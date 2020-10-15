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
        public JsonStaffOperations()
        {
            ReturnList();

        }

        static private readonly JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
        private List<Staffs> StaffList {get;set;}

        public List<Staffs> PopulateList()
        {
            return StaffList;
        }
        public void WriteData(List<Staffs>Stafflist)
        {
            StaffList = Stafflist;
            string jsonline = JsonConvert.SerializeObject(StaffList.ToArray(), Formatting.Indented, settings);
            File.WriteAllText(ConfigurationManager.AppSettings["Jsonfile"], string.Empty);
            File.WriteAllText(ConfigurationManager.AppSettings["Jsonfile"], jsonline);
        }

        private void ReturnList()
        {
            if (!File.Exists(ConfigurationManager.AppSettings["Jsonfile"]))
            {
                TextWriter tw = new StreamWriter(ConfigurationManager.AppSettings["xmlfile"]);
                tw.Close();
            }
            try
            {
                string Jsonstring = File.ReadAllText(ConfigurationManager.AppSettings["Jsonfile"]);
                StaffList = JsonConvert.DeserializeObject<List<Staffs>>(Jsonstring, settings);
            }
            catch
            {
                 StaffList= new List<Staffs>();
            }
        }
    }
}