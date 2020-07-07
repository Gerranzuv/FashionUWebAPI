using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControlPanel.ViewModels
{
    public class PaymnetViewModel
    {
        [Display(Name = "Status")]
        public string Status { get; set; }

        public String prodcutCode { get; set; }

        public String productType { get; set; }


        public String CompanyName { get; set; }
        public int CompanyId { get; set; }

        public double Amount { get; set; }
        public string Method { get; set; }

        public string currency { get; set; }

        public double UserAmount { get; set; }

        public double CompanyAmount { get; set; }

        public DateTime CreationDate { get; set; }
    }
}