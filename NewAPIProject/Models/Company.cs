using NewAPIProject.Extras;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewAPIProject.Models
{
    public class Company:BasicModel
    {
        public String name { get; set;}

        public bool isActive { get; set; }

        public String CompanyUserId { get; set; }

        public ApplicationUser CompanyUser { get; set; }

        [Display (Name ="Company Ratio")]
        public double CompanyRatio { get; set; }
    }
}