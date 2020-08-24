using System;

namespace Staffs
{
    class Program
    {
        static void Main(string[] args)
        {
            string select;
            JsonStaffOperations Staffs=new JsonStaffOperations();
            do
            {
                Console.WriteLine("\nENTER '1' FOR DATA ENTRY\nENTER '2' TO VIEW  DETAILS OF ALL STAFF\nENTER '3' TO VIEW STAFF DETAILS IN SPECIFIC\nENTER '4' TO DELETE STAFF DETAILS\nENTER '5' TO UPDATE STAFF DETAILS \nENTER '9' TO EXIT");
                select = Console.ReadLine();
                switch (select)
                {
                    case "1":
                        Staffs.EnterData();
                        break;
                    case "2":
                        Staffs.View();
                        break;
                    case "3":
                        int viewid =StaffOperations.ReturnId();
                        Staffs.ViewOne(viewid);
                        break;
                    case "4":
                        int deleteid = StaffOperations.ReturnId();
                        Staffs.Delete(deleteid);
                        break;
                    case "5":
                        int updateid = StaffOperations.ReturnId();
                        Staffs.Update(updateid);
                        break;
                    case "9":
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