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
        string filepath =ConfigurationManager.AppSettings["Jsonfile"];
        private readonly JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
        string tempfile =ConfigurationManager.AppSettings["tempfile"];
        public void EnterData()
        {
            if (File.Exists(filepath))
            {
                string Jsonstring = File.ReadAllText(filepath);
                List<Staffs> StaffList = JsonConvert.DeserializeObject<List<Staffs>>(Jsonstring, settings);
                Staffs staff = FileOperations.JsonEnterStaff();
                StaffList.Add(staff);
                string jsonline = JsonConvert.SerializeObject(StaffList.ToArray(), Formatting.Indented, settings);
                File.WriteAllText(filepath, string.Empty);
                File.WriteAllText(filepath, jsonline);
            }
            else
            {
                List<Staffs> StaffList = new List<Staffs>();
                Staffs staff = FileOperations.JsonEnterStaff();
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
                string jsonline = JsonConvert.SerializeObject(FileOperations.UpdateStaff(StaffList,index).ToArray(), Formatting.Indented, settings);
                File.WriteAllText(filepath, jsonline);
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
    public static int IdValue()
        {
            int largest = 0;
            string filepath =ConfigurationManager.AppSettings["Jsonfile"];
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
    }
}