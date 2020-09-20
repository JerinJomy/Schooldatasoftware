using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace Staffs
{
    public class StaffDB : IStaffOperations
    {
        public StaffDB()
        {
            ReturnStaffList();
        }
        public List<Staffs> StaffList { get; set; }



        public List<Staffs> PopulateList()
        {
            return StaffList;
        }


        public void WriteData(List<Staffs> Stafflist)
        {
            StaffList = Stafflist;
            DataTable StaffTable = ReturnStaffTable(StaffList);
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionstring")))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("proc_InsertStaffs", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@stafftable", StaffTable));
                    SqlDataAdapter adap = new SqlDataAdapter();
                    adap.InsertCommand = cmd;
                    adap.InsertCommand.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        private void ReturnStaffList()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionstring")))
                {
                    List<Staffs> templist = new List<Staffs>();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("proc_staffdata_getall", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dreader = cmd.ExecuteReader();
                    while (dreader.Read())
                    {
                        templist.Add(GetStaff(dreader));
                    }
                    conn.Close();
                    StaffList = templist;
                }
            }
            catch
            {
                StaffList = new List<Staffs>();
            }
        }
        private static Staffs GetStaff(SqlDataReader dreader)
        {
            int id = Convert.ToInt32(dreader["staffid"]);
            int stype = Convert.ToInt32(dreader["typeno"]);
            StaffType stafftype = (StaffType)stype;
            string name = Convert.ToString(dreader["name"]);
            string phone = Convert.ToString(dreader["phone"]);
            string email = Convert.ToString(dreader["email"]);
            if (stafftype == StaffType.TEACHINGSTAFF)
            {
                string classname = Convert.ToString(dreader["classname"]);
                string subject = Convert.ToString(dreader["subject"]);
                Staffs Staff = new TeachingStaffs(stafftype, name, phone, email, classname, subject, id);
                return Staff;
            }
            else if (stafftype == StaffType.ADMINISTRATIVESTAFF)
            {
                string designation_a = Convert.ToString(dreader["designation_a"]);
                Staffs staff = new AdministrativeStaff(stafftype, name, phone, email, id, designation_a);
                return staff;
            }
            else if (stafftype == StaffType.SUPPORTSTAFF)
            {
                string designation_s = Convert.ToString(dreader["designation_s"]);
                Staffs staff = new SupportStaffs(stafftype, name, phone, email, id, designation_s);
                return staff;
            }
            else
            {
                return null;
            }
        }
        private static DataTable ReturnStaffTable(List<Staffs> StaffList)
        {
            DataTable StaffTable = new DataTable();
            StaffTable.Columns.Add("staffid", typeof(int));
            StaffTable.Columns.Add("typeno", typeof(int));
            StaffTable.Columns.Add("name", typeof(string));
            StaffTable.Columns.Add("phone", typeof(string));
            StaffTable.Columns.Add("email", typeof(string));
            StaffTable.Columns.Add("designation_a", typeof(string));
            StaffTable.Columns.Add("designation_s", typeof(string));
            StaffTable.Columns.Add("classname", typeof(string));
            StaffTable.Columns.Add("subject", typeof(string));
            foreach (Staffs staff in StaffList)
            {
                StaffType stafftype = staff.StaffType;
                int stype = (int)stafftype;
                switch (stafftype)
                {
                    case StaffType.ADMINISTRATIVESTAFF:
                        StaffTable.Rows.Add(staff.Id, stype, staff.Name, staff.Phone, staff.Email, ((AdministrativeStaff)staff).Designation, null, null, null);
                        break;
                    case StaffType.TEACHINGSTAFF:
                        StaffTable.Rows.Add(staff.Id, stype, staff.Name, staff.Phone, staff.Email, null, null, ((TeachingStaffs)staff).ClassName, ((TeachingStaffs)staff).Subject);
                        break;
                    case StaffType.SUPPORTSTAFF:
                        StaffTable.Rows.Add(staff.Id, stype, staff.Name, staff.Phone, staff.Email, null, ((SupportStaffs)staff).Designation, null, null);
                        break;
                }
            }
            return StaffTable;
        }
        public static int GetId()
        {
           using(SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionstring")))
            {
                conn.Open();
                string sql = "SELECT Cast(IDENT_CURRENT('staffs') As Int)";
                SqlCommand cmd= new SqlCommand(sql, conn);
                SqlDataReader dreader = cmd.ExecuteReader();
                dreader.Read();
                int id = (int)dreader.GetValue(0)+1;
                return id;
            }
        }
    }
}