using System;
namespace Staffs
{
    interface IStaffOperations
    {
        void EnterData();
        void View();
        void ViewOne(int id);
        void Update(int id);
        void Delete(int id);
        void Deserialize();
    }
}