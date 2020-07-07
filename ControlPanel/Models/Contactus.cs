using ControlPanel.Extras;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControlPanel.Models
{
    public class Contactus:BasicModel
    {
        public string Title { get; set;}

        public string Text { get; set; }

        public string Email { get; set; }

        [Display (Name ="Phone Number")]
        public string phoneNumber { get; set; }
    }
}