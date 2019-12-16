using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAPIProject.ViewModels
{
    public class VerifyEmail
    {
        public String code { get; set; }
        public string userId { get; set; }

        public string email { get; set; }

    }

}