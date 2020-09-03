using System;
using System.Data.SqlClient;

namespace Staffs
{
    public static class StaffDB
    {
        public static void EnterData()
        {
            string connect = @"Data Source = LAPTOP-TFS7IR78\SQLEXPRESS; Initial Catalog = staffsdata; Integrated Security=SSPI";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            int id = GetId("Staffs", "staffid");
            Console.WriteLine(id);
            Console.WriteLine("enter '1' for Administrative Staff\nenter '2' for Teaching Staff \nenter '3' for Support Staff");
            string stype = Console.ReadLine();
            Console.WriteLine("enter the  name");
            string name = Console.ReadLine();
            Console.WriteLine("enter the phone no");
            string phone = Console.ReadLine();
            Console.WriteLine("enter the email id");
            string email = Console.ReadLine();
            string sql = string.Format("insert into STAFFS (STAFFID,TYPENO,NAME,PHONE,EMAIL) values({0},{1},'{2}','{3}','{4}');", id, stype, name, phone, email);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adap = new SqlDataAdapter();
            adap.InsertCommand = new SqlCommand(sql, conn);
            adap.InsertCommand.ExecuteNonQuery();
            adap.Dispose();
            cmd.Dispose();
            if (stype == "1")
            {
                int staffno = GetId("ADMINISTRATIVESTAFF", "staffno");
                Console.WriteLine("Enter the designation of the staff");
                string designation = Console.ReadLine();
                sql = string.Format("insert into ADMINISTRATIVESTAFF (STAFFNO,DESIGNATION,STAFFID) values({0},'{1}',{2});", staffno, designation, id);
                cmd = new SqlCommand(sql, conn);
                adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand(sql, conn);
                adap.InsertCommand.ExecuteNonQuery();
                adap.Dispose();
                cmd.Dispose();
            }
            else if (stype == "2")
            {
                int staffno = GetId("TEACHINGSTAFF", "staffno");
                Console.WriteLine("enter the classname");
                string classname = Console.ReadLine();
                Console.WriteLine("enter the subject taught");
                string subject = Console.ReadLine();
                sql = string.Format("insert into TEACHINGSTAFF (STAFFNO,CLASSNAME,SUBJECT,STAFFID) values({0},'{1}','{2}','{3}');", staffno, classname, subject, id);
                cmd = new SqlCommand(sql, conn);
                adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand(sql, conn);
                adap.InsertCommand.ExecuteNonQuery();
                adap.Dispose();
                cmd.Dispose();
            }
            else if (stype == "3")
            {
                int staffno = GetId("SUPPORTSTAFF", "staffno");
                Console.WriteLine("Enter the designation of the staff");
                string designation = Console.ReadLine();
                sql = string.Format("insert into SUPPORTSTAFF (STAFFNO,DESIGNATION,STAFFID) values({0},'{1}',{2});", staffno, designation, id);
                cmd = new SqlCommand(sql, conn);
                adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand(sql, conn);
                adap.InsertCommand.ExecuteNonQuery();
                adap.Dispose();
                cmd.Dispose();
            }
        }
        public static void View()
        {
            string connect = @"Data Source = LAPTOP-TFS7IR78\SQLEXPRESS; Initial Catalog = staffsdata; Integrated Security=SSPI";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            string sql = "select s.staffid ,st.stafftype,s.name,s.phone,s.email,a.designation,sp.designation" +
                         ", t.classname,t.subject from STAFFS s inner join stafftype st on s.typeno = st.typeno " +
                         "full join ADMINISTRATIVESTAFF a on a.staffid = s.staffid full join supportstaff sp " +
                         "on sp.staffid = s.staffid full join teachingstaff t on t.staffid = s.staffid;";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dreader = cmd.ExecuteReader();
            while (dreader.Read())
            {
                string staffdetails = "StaffId:" + dreader.GetValue(0) + "\n" + "StaffType:" + dreader.GetValue(1) + "\n" + "Name:" + dreader.GetValue(2)
                                + "\n" + "Phone:" + dreader.GetValue(3) + "\n" + "Email:" + dreader.GetValue(4) + "\n";
                string stafftype = Convert.ToString(dreader.GetValue(1));
                switch (stafftype)
                {
                    case "administrative staff":
                        staffdetails = staffdetails + "Designation:" + dreader.GetValue(5) + "\n";
                        break;
                    case "support staff":
                        staffdetails = staffdetails + "Designation:" + dreader.GetValue(6) + "\n";
                        break;
                    case "teachining staff":
                        staffdetails = staffdetails + "Classname:" + dreader.GetValue(7) + "\n" + "Subject:" + dreader.GetValue(8) + "\n";
                        break;
                }
                Console.WriteLine(staffdetails);
            }
            dreader.Close();
            conn.Close();
            cmd.Dispose();
        }
        public static void ViewOne(int id)
        {
            string connect = @"Data Source = LAPTOP-TFS7IR78\SQLEXPRESS; Initial Catalog = staffsdata; Integrated Security=SSPI";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            string sqlwhere = string.Format(" where s.staffid={0};", id);
            string sql = "select s.staffid ,st.stafftype,s.name,s.phone,s.email,a.designation,sp.designation" +
                         ", t.classname,t.subject from STAFFS s inner join stafftype st on s.typeno = st.typeno " +
                         "full join ADMINISTRATIVESTAFF a on a.staffid = s.staffid full join supportstaff sp " +
                         "on sp.staffid = s.staffid full join teachingstaff t on t.staffid = s.staffid " + sqlwhere;
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dreader = cmd.ExecuteReader();
            dreader.Read();
            string staffdetails = "StaffId:" + dreader.GetValue(0) + "\n" + "StaffType:" + dreader.GetValue(1) + "\n" + "Name:" + dreader.GetValue(2)
                            + "\n" + "Phone:" + dreader.GetValue(3) + "\n" + "Email:" + dreader.GetValue(4) + "\n";
            string stafftype = Convert.ToString(dreader.GetValue(1));
            switch (stafftype)
            {
                case "administrative staff":
                    staffdetails = staffdetails + "Designation:" + dreader.GetValue(5) + "\n";
                    break;
                case "support staff":
                    staffdetails = staffdetails + "Designation:" + dreader.GetValue(6) + "\n";
                    break;
                case "teachining staff":
                    staffdetails = staffdetails + "Classname:" + dreader.GetValue(7) + "\n" + "Subject:" + dreader.GetValue(8) + "\n";
                    break;
            }
            Console.WriteLine(staffdetails);
            dreader.Close();
            conn.Close();
            cmd.Dispose();
        }
        private static int GetId(string stafftype,string idtype)
        {
            string connect = @"Data Source = LAPTOP-TFS7IR78\SQLEXPRESS; Initial Catalog = staffsdata; Integrated Security=SSPI";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            string sql = string.Format("select max({0}) from {1};",idtype,stafftype);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dreader = cmd.ExecuteReader();
            dreader.Read();
            int id = Convert.ToInt32(dreader.GetValue(0)) + 1;
            dreader.Close();
            cmd.Dispose();
            return id;
        }
    }
}
