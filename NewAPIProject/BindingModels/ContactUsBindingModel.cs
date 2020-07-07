using NewAPIProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAPIProject.ViewModels
{
    public class ContactUsBindingModel
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public string Email { get; set; }

        public string phoneNumber { get; set; }

        public string Address { get; set; }

        public int photoId { get; set; }

        public string size { get; set; }

    }
}