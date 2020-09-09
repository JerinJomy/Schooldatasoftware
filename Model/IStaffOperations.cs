using System;
using System.Collections.Generic;
namespace Staffs
{
    public interface IStaffOperations
    {
        List<Staffs> PopulateList();
        void WriteData(List<Staffs> Stafflist);
    }
}