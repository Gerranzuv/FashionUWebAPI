using NewAPIProject.Extras;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewAPIProject.Models
{
    public class Picklist : BasicModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

                
        [Display(Name = "Code")]
        public string Code { get; set; }

        public List<PicklistItem> items { get; set; }
      
    }
}