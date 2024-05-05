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
        PersonalInfor,
        FamilyInfor,
        TrainingProcess,
        WorkProcess,
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
        PositionStaff,
        // ----------------------- Công cán -----------------------
        Tracking,
        ListTracking,
        // ----------------------- Lương thưởng -----------------------
        AdvanceSalary
    }
}
