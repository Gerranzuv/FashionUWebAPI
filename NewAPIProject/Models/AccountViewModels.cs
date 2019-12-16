using NewAPIProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Security;

namespace NewAPIProject.Models
{
    // Models returned by AccountController actions.

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModificationDate { get; set; }
        
        public String Sex { get; set; }
        
        public String Country { get; set; }

        public String Currency { get; set; }

        public String Language { get; set; }

        public String Name { get; set; }

        public String PhoneNumber { get; set; }
        public List<string> UserRoles { get; set; }

    }

    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
