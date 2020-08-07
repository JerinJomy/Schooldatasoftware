using System;

namespace Staffs
{
    class Program
    {
        static void Main(string[] args)
        {
            string select;
            do
            {
                Console.WriteLine("\nENTER '1' FOR DATA ENTRY\nENTER '2' TO VIEW  DETAILS OF ALL STAFF\nENTER '3' TO VIEW STAFF DETAILS IN SPECIFIC\nENTER '4' TO DELETE STAFF DETAILS\nENTER '5' TO UPDATE STAFF DETAILS \nENTER '9' TO EXIT");
                select = Console.ReadLine();
                switch (select)
                {
                    case "1":
                        StaffOperations.EnterData();
                        break;
                    case "2":
                        StaffOperations.View();
                        break;
                    case "3":
                        Console.WriteLine("ENTER THE STAFF id");
                        int viewid = Convert.ToInt32(Console.ReadLine());
                        StaffOperations.ViewOne(viewid);
                        break;
                    case "4":
                        Console.WriteLine("ENTER THE STAFF id");
                        int deleteid = Convert.ToInt32(Console.ReadLine());
                        StaffOperations.Delete(deleteid);
                        break;
                    case "5":
                        Console.WriteLine("ENTER THE STAFF id");
                        int updateid = Convert.ToInt32(Console.ReadLine());
                        StaffOperations.UpdateData(updateid);
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