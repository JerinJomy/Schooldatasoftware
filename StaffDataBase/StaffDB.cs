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
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings.Get("connect"));
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
                string sql = string.Format("insert into STAFFS (STAFFID,TYPENO,NAME,PHONE,EMAIL) values({0},{1},'{2}','{3}','{4}');", staff.Id, stype, staff.Name, staff.Phone, staff.Email);
                SqlCommand cmd1 = new SqlCommand(sql, conn);
                adap = new SqlDataAdapter();
                adap.InsertCommand = new SqlCommand(sql, conn);
                adap.InsertCommand.ExecuteNonQuery();
                adap.Dispose();
                cmd1.Dispose();
                switch(stafftype)
                {
                    case StaffType.ADMINISTRATIVESTAFF:
                        sql = string.Format("insert into ADMINISTRATIVESTAFF (STAFFNO,DESIGNATION_A,STAFFID) values({0},'{1}',{2});", staffno_a, ((AdministrativeStaff)staff).Designation, staff.Id);
                        staffno_a = staffno_a + 1;
                        cmd1 = new SqlCommand(sql, conn);
                        adap = new SqlDataAdapter();
                        adap.InsertCommand = new SqlCommand(sql, conn);
                        adap.InsertCommand.ExecuteNonQuery();
                        adap.Dispose();
                        cmd1.Dispose();
                        break;
                    case StaffType.TEACHINGSTAFF:
                        sql = string.Format("insert into TEACHINGSTAFF (STAFFNO,CLASSNAME,SUBJECT,STAFFID) values({0},'{1}','{2}','{3}');", staffno_t, ((TeachingStaffs)staff).ClassName, ((TeachingStaffs)staff).Subject, staff.Id);
                        staffno_t = staffno_t + 1;
                        cmd1 = new SqlCommand(sql, conn);
                        adap = new SqlDataAdapter();
                        adap.InsertCommand = new SqlCommand(sql, conn);
                        adap.InsertCommand.ExecuteNonQuery();
                        adap.Dispose();
                        cmd1.Dispose();
                        break;
                    case StaffType.SUPPORTSTAFF:
                        sql = string.Format("insert into SUPPORTSTAFF (STAFFNO,DESIGNATION_S,STAFFID) values({0},'{1}',{2});", staffno_s, ((SupportStaffs)staff).Designation, staff.Id);
                        staffno_s = staffno_s + 1;
                        cmd1 = new SqlCommand(sql, conn);
                        adap = new SqlDataAdapter();
                        adap.InsertCommand = new SqlCommand(sql, conn);
                        adap.InsertCommand.ExecuteNonQuery();
                        adap.Dispose();
                        cmd1.Dispose();
                        break;
                }
            }
            

        }
        public static List<Staffs>ReturnList()
        {
            List<Staffs> StaffList = new List<Staffs>();
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings.Get("connect"));
                conn.Open();
                SqlCommand cmd = new SqlCommand("staffdata_getall", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dreader = cmd.ExecuteReader();
                while (dreader.Read())
                {
                    int id = Convert.ToInt32(dreader["staffid"]);
                    int stype = Convert.ToInt32(dreader["typeno"]);
                    StaffType stafftype = (StaffType)stype;
                    string name = Convert.ToString(dreader["name"]);
                    string phone = Convert.ToString(dreader["phone"]);
                    string email = Convert.ToString(dreader["email"]);
                    switch (stafftype)
                    {
                        case StaffType.ADMINISTRATIVESTAFF:
                            string designation_a = Convert.ToString(dreader["designation_a"]);
                            StaffList.Add(new AdministrativeStaff(stafftype, name, phone, email, id, designation_a));
                            break;
                        case StaffType.TEACHINGSTAFF:
                            string classname = Convert.ToString(dreader["classname"]);
                            string subject = Convert.ToString(dreader["subject"]);
                            StaffList.Add(new TeachingStaffs(stafftype, name, phone, email, classname, subject, id));
                            break;
                        case StaffType.SUPPORTSTAFF:
                            string designation_s = Convert.ToString(dreader["designation_s"]);
                            StaffList.Add(new SupportStaffs(stafftype, name, phone, email, id, designation_s));
                            break;
                    }
                }
                conn.Close();
                return StaffList;
            }
            catch
            {
                return StaffList;
            }
        }
    }
}
