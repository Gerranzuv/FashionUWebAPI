using NewAPIProject.Extras;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewAPIProject.Models
{
    public class ShippingRequest : BasicModel
    {
        [Display(Name = "Status")]
        public string Status { get; set; }

        public Product prodcut { get; set; }

        public int productId { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Scheduled Date")]
        public DateTime scheduledDate { get; set; }

        

    }
}