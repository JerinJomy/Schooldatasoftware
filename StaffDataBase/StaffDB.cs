using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace Staffs
{
    public class StaffDB : IStaffOperations
    {
        List<Staffs> StaffList = ReturnList();
        public void Delete(int id)
        {
            StaffOperations.Delete(id, StaffList);
        }

        public void EnterData()
        {
            StaffList.Add(StaffOperations.EnterData(StaffList));
        }

        public void Update(int id)
        {
            StaffOperations.UpdateData(id, StaffList);
        }

        public void View()
        {
            StaffOperations.View(StaffList); ;
        }

        public void ViewOne(int id)
        {
            StaffOperations.ViewOne(id, StaffList);
        }

        public void WriteData()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionstring"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("ClearTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adap = new SqlDataAdapter();
            adap.InsertCommand = new SqlCommand("ClearTable", conn);
            adap.InsertCommand.ExecuteNonQuery();
            cmd.Dispose();
            adap.Dispose();
            int staffno_a = 1, staffno_t = 1, staffno_s = 1;
            foreach (Staffs staff in StaffList)
            {
                StaffType  stafftype= staff.StaffType;
                int stype = (int)stafftype; 
                SqlCommand cmd1 = new SqlCommand("Insertstaff", conn);
                cmd1.Parameters.AddWithValue("@staffid", SqlDbType.Int).Value = staff.Id;
                cmd1.Parameters.AddWithValue("@typeno", SqlDbType.Int).Value = stype;
                cmd1.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = staff.Name;
                cmd1.Parameters.AddWithValue("@phone", SqlDbType.VarChar).Value = staff.Phone;
                cmd1.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = staff.Email;
                adap = new SqlDataAdapter();
                adap.InsertCommand = cmd1;
                cmd1.CommandType = CommandType.StoredProcedure;
                adap.InsertCommand.ExecuteNonQuery();   
                adap.Dispose();
                cmd1.Dispose();
                switch(stafftype)
                {
                    case StaffType.ADMINISTRATIVESTAFF:
                        cmd1 = new SqlCommand("Insert_Astaff", conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@staffno", SqlDbType.Int).Value = staffno_a;
                        cmd1.Parameters.AddWithValue("@designation_a", SqlDbType.VarChar).Value = ((AdministrativeStaff)staff).Designation;
                        cmd1.Parameters.AddWithValue("@staffid", SqlDbType.Int).Value = staff.Id;
                        adap = new SqlDataAdapter();
                        adap.InsertCommand = cmd1;
                        adap.InsertCommand.ExecuteNonQuery();
                        adap.Dispose();
                        cmd1.Dispose();
                        staffno_a = staffno_a + 1;
                        break;
                    case StaffType.TEACHINGSTAFF:
                        cmd1 = new SqlCommand("Insert_Tstaff", conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@staffno", SqlDbType.Int).Value = staffno_t;
                        cmd1.Parameters.AddWithValue("@classname", SqlDbType.VarChar).Value = ((TeachingStaffs)staff).ClassName;
                        cmd1.Parameters.AddWithValue("@subject", SqlDbType.VarChar).Value = ((TeachingStaffs)staff).Subject;
                        cmd1.Parameters.AddWithValue("@staffid", SqlDbType.Int).Value = staff.Id;
                        adap = new SqlDataAdapter();
                        adap.InsertCommand = cmd1;
                        adap.InsertCommand.ExecuteNonQuery();
                        adap.Dispose();
                        cmd1.Dispose();
                        staffno_t = staffno_t + 1;
                        break;
                    case StaffType.SUPPORTSTAFF:
                        cmd1 = new SqlCommand("Insert_Sstaff", conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@staffno", SqlDbType.Int).Value = staffno_s;
                        cmd1.Parameters.AddWithValue("@designation_s", SqlDbType.VarChar).Value = ((SupportStaffs)staff).Designation;
                        cmd1.Parameters.AddWithValue("@staffid", SqlDbType.Int).Value = staff.Id;
                        adap = new SqlDataAdapter();
                        adap.InsertCommand = cmd1;
                        adap.InsertCommand.ExecuteNonQuery();
                        adap.Dispose();
                        cmd1.Dispose();
                        staffno_s = staffno_s + 1;
                        break;
                }
            }
            

        }
        private static List<Staffs>ReturnList()
        {
            List<Staffs> StaffList = new List<Staffs>();
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionstring"));
                conn.Open();
                SqlCommand cmd = new SqlCommand("staffdata_getall", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dreader = cmd.ExecuteReader();
                while (dreader.Read())
                {
                    StaffList.Add(AddStaff(dreader));
                }
                conn.Close();
                return StaffList;
            }
            catch
            {
                return StaffList;
            }
        }
        private static Staffs AddStaff(SqlDataReader dreader)
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
    }
}
