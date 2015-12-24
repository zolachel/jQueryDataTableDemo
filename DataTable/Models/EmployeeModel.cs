using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataTable.Models
{
    public class EmployeeModel
    {
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return string.Format("{0} {1} {2}", FirstName, MidName == null ? string.Empty : MidName , LastName) ;
            }
        }
        public string SSN { get; set; }
        public DateTime? Birthday { get; set; }
        public string DisplayBirthDay { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public decimal? Salary { get; set; }
        public string SupervisorSSN { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }

    }
}