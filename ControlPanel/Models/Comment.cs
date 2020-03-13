using ControlPanel.Extras;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControlPanel.Models
{
    public class Comment:BasicModel
    {
        [Display(Name = "Text")]
        public String Text { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}