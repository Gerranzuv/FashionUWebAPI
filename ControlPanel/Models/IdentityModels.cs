using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;

namespace ControlPanel.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Last Modification Date")]
        public DateTime LastModificationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Sex")]
        public string Sex { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Language")]
        public string Language { get; set; }


        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        public Boolean companyUser { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Product> Products { get; set; }

        public System.Data.Entity.DbSet<ControlPanel.Models.Company> Companies { get; set; }

        public System.Data.Entity.DbSet<ControlPanel.Models.Payment> Payments { get; set; }

        public System.Data.Entity.DbSet<ControlPanel.Models.Attachment> Attachments { get; set; }

        public System.Data.Entity.DbSet<ControlPanel.Models.ShippingRequest> ShippingRequests { get; set; }

        public System.Data.Entity.DbSet<ControlPanel.Models.Contactus> Contactus { get; set; }

        public System.Data.Entity.DbSet<ControlPanel.Models.EmailLog> EmailLogs { get; set; }

        public System.Data.Entity.DbSet<ControlPanel.Models.SystemParameter> SystemParameters { get; set; }

        public System.Data.Entity.DbSet<ControlPanel.Models.UsersDeviceTokens> UsersDeviceTokens { get; set; }
    }
}