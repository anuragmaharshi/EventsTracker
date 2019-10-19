using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManager.Entities;
using LiteDB;

namespace EventManager.Services
{
    public class OfficerRepo : IOfficers
    {
        private string connectionString = @"C:\\Database\\Events.db";
        public void AddOfficer(Officers officer)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var col = db.GetCollection<Officers>("Officers");
                officer.Id = Guid.NewGuid();
                col.Insert(officer);
            }
        }

        public void DeleteOfficer(Officers officer)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var col = db.GetCollection<Officers>("Officers");
                col.Delete(officer.Id);
            }
        }
       

        public void UpdateOfficer(Officers officer)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var col = db.GetCollection<Officers>("Officers");
                col.Update(officer);
            }
        }

        public List<Officers> GetAllOfficers()
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var col = db.GetCollection<Officers>("Officers");
                return col.FindAll().ToList();
            }
        }
    }
}
