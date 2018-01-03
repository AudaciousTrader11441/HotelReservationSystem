using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HotelReservationSystem.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

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

        public System.Data.Entity.DbSet<HotelReservationSystem.Areas.Managers.Models.VMhotels> VMhotels { get; set; }

        public System.Data.Entity.DbSet<HotelReservationSystem.Areas.Customers.Models.AnationsCustomer> AnationsCustomers { get; set; }

        public System.Data.Entity.DbSet<HotelReservationSystem.Areas.Customers.Models.VMECustomer> VMECustomers { get; set; }

        public System.Data.Entity.DbSet<HotelReservationSystem.Areas.Customers.Models.AnationsBooking> AnationsBookings { get; set; }

        public System.Data.Entity.DbSet<HotelReservationSystem.Areas.Managers.Models.VMEmanager> VMEmanagers { get; set; }

        public System.Data.Entity.DbSet<HotelReservationSystem.Areas.Managers.Models.AnationsManager> AnationsManagers { get; set; }
    }
}