using NewAPIProject.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAPIProject.Models
{
    public class Contactus:BasicModel
    {
        public String Title { get; set;}

        public String Text { get; set; }

        public String RelatedUserId { get; set; }

        public ApplicationUser RelatedUser { get; set; }
    }
}