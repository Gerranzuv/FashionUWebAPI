﻿using NewAPIProject.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAPIProject.Models
{
    public class Contactus:BasicModel
    {
        public string Title { get; set;}

        public string Text { get; set; }

        public string Email { get; set; }

        public string phoneNumber { get; set; }
    }
}