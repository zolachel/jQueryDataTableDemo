//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataTable.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Dependent
    {
        public string EmployeeSSN { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Relationship { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}