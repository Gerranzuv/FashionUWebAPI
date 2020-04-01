using NewAPIProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAPIProject.ViewModels
{
    public class CompanyViewModel
    {
        public int id { get; set; }

        public bool isActive { get; set; }

        public String CompanyUserId { get; set; }

        public String CompanyUserName { get; set; }
    }
}