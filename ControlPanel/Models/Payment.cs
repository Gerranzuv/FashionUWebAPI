using ControlPanel.Extras;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControlPanel.Models
{
    public class Payment : BasicModel
    {
        [Display(Name = "Status")]
        public string Status { get; set; }

        public Product prodcut { get; set; }

        public int productId { get; set; }

        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public double Amount { get; set; }
        public string Method { get; set; }

        public string currency { get; set; }
    }
}