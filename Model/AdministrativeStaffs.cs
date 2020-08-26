using System;
namespace Staffs
{
    public class AdministrativeStaff : Staffs
    {
        public string Designation { get; set; }
        public AdministrativeStaff(StaffType staffType, string name, string phone, string email, int id, String designation) : base(staffType, name, phone, email, id)
        {
            Designation = designation;
        }
        public AdministrativeStaff()
        {
            
        }
        public void UpdateAdministrative(string name, string phone, string email, string designation)
        {
            base.Update(name, phone, email);
            Designation = designation;
        }
    }
}