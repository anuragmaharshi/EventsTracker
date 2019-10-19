using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventManager.Entities
{
    public class Officers
    {
        public Guid Id { get; set; }

        public OfficerGradeType OfficerGrade { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

       
    }
}
