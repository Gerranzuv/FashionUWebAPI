using NewAPIProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAPIProject.ViewModels
{
    public class ProductViewModel
    {
        public int id { get; set; }
        public List<Attachment> Photos { get; set; }
    }
}