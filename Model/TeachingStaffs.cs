using System;
namespace Staffs
{
    public class TeachingStaffs : Staffs
    {
        public string ClassName { get; set; }
        public string Subject { get; set; }

        public TeachingStaffs(StaffType staffType, string name, string phone, string email, string classname, string subject, int id) : base(staffType, name, phone, email, id)
        {
            ClassName = classname;
            Subject = subject;
        }

        public void UpdateTeaching(string name, string phone, string email, string classname, string subject)
        {
            base.Update(name, phone, email);
            ClassName = classname;
            Subject = subject;

        }
    }
}