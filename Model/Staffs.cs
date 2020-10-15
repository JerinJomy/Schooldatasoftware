using System;

namespace Staffs
{
    public class Staffs
    {

        public string Name { get; set; }
        public StaffType StaffType { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }


        public Staffs(StaffType staffType, string name, string phone, string email, int id)
        {
            StaffType = staffType;
            Name = name;
            Phone = phone;
            Email = email;
            Id = id;

        }
        public Staffs()
        {

        }
        public void Update(string name, string phone, string email)
        {

            Name = name;
            Phone = phone;
            Email = email;
        }

    }


}