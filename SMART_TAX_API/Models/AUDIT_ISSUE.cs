﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMART_TAX_API.Models
{
    public class AUDIT_ISSUE
    {
        public int ID { get; set; }
        public int COMPANY_ID { get; set; }
        public string ASSESSMENT_YEAR { get; set; }
        public string ISSUE { get; set; }
        //[DataType(DataType.Date)]
        public DateTime? RAISED_DATE { get; set; }
        //[DataType(DataType.Date)]
        public DateTime? DUE_DATE { get; set; }
        public string STATUS { get; set; }
        public string SEVERITY { get; set; }
        public string RESOLUTION { get; set; }
        //[DataType(DataType.Date)]
        public DateTime? CLOSURE_DATE { get; set; } = null;


    }
}
