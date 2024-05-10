using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.UI.Defines
{
    public enum EViewTypes
    {
        Login,
        Register,
        MainContent,
        //---------------------- Xem, sửa, xóa ----------------------
        PersonalInfor,
        FamilyInfor,
        TrainingProcess,
        WorkProcess,
        //---------------------- Thêm ----------------------
        AddPersonal,
        AddFamilyInfor,
        AddTrainingProcess,
        AddWorkProcess,
        // ---------------------- Khen thưởng, kỷ luật ----------------------
        Remunerative,
        ListRemunerative,
        Discipline,
        // --------------------------------------------
        ListStaff,
        StaffCV,
        Department,
        Position,
        AddCV,
        Contract,
        ListContract,
        ChildContent,
        AddChildContent,
        PositionStaff,
        // ----------------------- Công cán -----------------------
        Tracking,
        ImportTracking,
        ListTracking,
        // ----------------------- Lương thưởng -----------------------
        Salary,
        AdvanceSalary
    }
}
