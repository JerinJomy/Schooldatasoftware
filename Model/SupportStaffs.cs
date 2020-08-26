using System;
namespace Staffs
{
    public class SupportStaffs : Staffs
    {
        public string Designation { get; set; }
        public SupportStaffs(StaffType staffType, string name, string phone, string email, int id, string designation) : base(staffType, name, phone, email, id)
        {
            Designation = designation;
        }
        public SupportStaffs()
        {
            
        }
        public void UpdateSupport(string name, string phone, string email, string desgnation)
        {
            base.Update(name, phone, email);
            Designation = desgnation;
        }
    }
}