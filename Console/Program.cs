using System;
using System.Reflection;
using System.Collections.Generic;
using System.Configuration;

namespace Staffs
{
    class Program
    {
        static void Main(string[] args)
        {
            string objectselect = ConfigurationManager.AppSettings.Get("json");
            var objectType = Type.GetType(objectselect);
            IStaffOperations staff=Activator.CreateInstance(objectType) as IStaffOperations;
            List<Staffs> StaffList = new List<Staffs>();
            string select;
            do
            {
                Console.WriteLine("\nENTER '1' FOR DATA ENTRY\nENTER '2' TO VIEW  DETAILS OF ALL STAFF\nENTER '3' TO VIEW STAFF DETAILS IN SPECIFIC\nENTER '4' TO DELETE STAFF DETAILS\nENTER '5' TO UPDATE STAFF DETAILS \nENTER '9' TO EXIT");
                select = Console.ReadLine();
                switch (select)
                {
                    case "1":
                        staff.EnterData();
                        break;
                    case "2":
                        staff.View();
                        break;
                    case "3":
                        int viewid = StaffOperations.ReturnId();
                        staff.ViewOne(viewid);
                        break;
                    case "4":
                        int deleteid = StaffOperations.ReturnId();
                        staff.Delete(deleteid);
                        break;
                    case "5":
                        int updateid = StaffOperations.ReturnId();
                        staff.Update(updateid);
                        break;
                    case "9":
                        staff.Deserialize();
                        Console.WriteLine("PROGRAM ENDED");
                        break;
                    default:
                        Console.WriteLine("INVALID OPTION");
                        break;
                }
            }
            while (select != "9");

        }
    }
}

