using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventManager.Entities;
using LiteDB;

namespace EventManager.Services
{
    public class OfficerGradeRepo : IOfficerGrade
    {
        private string connectionString = @"C:\\Database\\Events.db";
        public void AddOfficerGradeAsync(OfficerGradeType officerGrade)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var col = db.GetCollection<OfficerGradeType>("OfficerGrades");
                officerGrade.Id = Guid.NewGuid();
                col.Insert(officerGrade);
            }
        }

        public List<OfficerGradeType> GetAllOfficerGrades()
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var col = db.GetCollection<OfficerGradeType>("OfficerGrades");

                return col.FindAll().ToList();
            }

            
        }
    }
}
