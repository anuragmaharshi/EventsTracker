using EventManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventManager.Services
{
    public interface IOfficerGrade
    {
        void AddOfficerGradeAsync(OfficerGradeType officerGrade);

        void UpdateOfficerGrade(OfficerGradeType officerGrade);

        void DeleteOfficerGrader(OfficerGradeType officerGrade);

        List<OfficerGradeType> GetAllOfficerGrades();
    }
}
