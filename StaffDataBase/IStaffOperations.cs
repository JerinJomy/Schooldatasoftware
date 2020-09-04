using System;
namespace Staffs
{
    public interface IStaffOperations
    {
        void EnterData();
        void View();
        void ViewOne(int id);
        void Update(int id);
        void Delete(int id);
        void WriteData();
    }
}