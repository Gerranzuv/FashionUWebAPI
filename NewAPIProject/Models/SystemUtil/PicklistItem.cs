using NewAPIProject.Extras;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewAPIProject.Models
{
    public class PicklistItem : BasicModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

                
        [Display(Name = "Code")]
        public string Code { get; set; }

        public int PicklistId { get; set; }

        public Picklist FatherPickList { get; set; }
    }
}