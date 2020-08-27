using System;
using System.Configuration;

namespace Staffs
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string select;
            do
            {
                Console.WriteLine("\nEnter 'j' to use JOSN file\nEnter 'x' to use XML file\nENTER '1' FOR DATA ENTRY\nENTER '2' TO VIEW  DETAILS OF ALL STAFF\nENTER '3' TO VIEW STAFF DETAILS IN SPECIFIC\nENTER '4' TO DELETE STAFF DETAILS\nENTER '5' TO UPDATE STAFF DETAILS \nENTER '9' TO EXIT");
                select = Console.ReadLine();
                switch (select)
                {
                    case "j":
                        FileOperations.JsonProgram();
                        break;
                    case "x":
                        FileOperations.XMLProgram();
                        break;
                    case "1":
                        StaffOperations.EnterData();
                        break;
                    case "2":
                        StaffOperations.View();
                        break;
                    case "3":
                        int viewid = StaffOperations.ReturnId();
                        StaffOperations.ViewOne(viewid);
                        break;
                    case "4":
                        int deleteid = StaffOperations.ReturnId();
                        StaffOperations.Delete(deleteid);
                        break;
                    case "5":
                        int updateid = StaffOperations.ReturnId();
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

