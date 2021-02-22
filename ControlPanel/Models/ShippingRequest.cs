﻿using ControlPanel.Extras;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControlPanel.Models
{
    public class ShippingRequest : BasicModel
    {
        [Display(Name = "Status")]
        public string Status { get; set; }

        public Product prodcut { get; set; }

        public int productId { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Scheduled Date")]
        public DateTime? scheduledDate { get; set; }


        public Payment Payment { get; set; }

        public int? PaymentId { get; set; }

        public DateTime? CancelationDate { get; set; }

        public string ReasonOfCancellation { get; set; }


        public string Email { get; set; }

        public string phoneNumber { get; set; }

        public string Address { get; set; }

        public int photoId { get; set; }

        public string size { get; set; }

        public int count { get; set; }
    }
}