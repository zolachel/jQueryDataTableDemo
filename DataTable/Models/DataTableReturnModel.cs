using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataTable.Models
{
    public class DataTableReturnModel<T>
    {
        public int draw { get; set; }
        public List<T> data { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }
}