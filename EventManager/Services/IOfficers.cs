using EventManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Services
{
    public interface IOfficers
    {
        void AddOfficer(Officers officer);

        void UpdateOfficer(Officers officerGrade);

        void DeleteOfficer(Officers officerGrade);

        List<Officers> GetAllOfficers();
    }
}
