using System;
using System.Collections.Generic;
using System.Text;

namespace ORMs.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Title { get; set; }

        public List<Territory> Territory { get; set; }
    }
}
