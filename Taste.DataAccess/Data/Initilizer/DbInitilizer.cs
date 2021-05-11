using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Taste.Models;
using Taste.Utility;

namespace Taste.DataAccess.Data.Initilizer
{
    public class DbInitilizer : IDbInitilizer
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private UserManager<IdentityUser> _userManagerService;
        private readonly RoleManager<IdentityRole> _roleManagerService;
        public DbInitilizer(ApplicationDbContext applicationDbContext,
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManagerService = userManager;
            _roleManagerService = roleManager;
        }
        public void Initilize()
        {
            try
            {
                if(_applicationDbContext.Database.GetPendingMigrations().Any())
                {
                    _applicationDbContext.Database.Migrate();
                }
            }
            catch (System.Exception)
            {

                throw;
            }

            if (_applicationDbContext.Roles.Any(c => c.Name == SD.ManagerRole))
            {
                return;
            }
            else
            {
                _roleManagerService.CreateAsync(new IdentityRole(SD.ManagerRole)).GetAwaiter().GetResult();
                _roleManagerService.CreateAsync(new IdentityRole(SD.CustomerRole)).GetAwaiter().GetResult();
                _roleManagerService.CreateAsync(new IdentityRole(SD.FrontDeskRole)).GetAwaiter().GetResult();
                _roleManagerService.CreateAsync(new IdentityRole(SD.KitchenRole)).GetAwaiter().GetResult();


                _userManagerService.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@iamtuse.com",
                    Email = "admin@iamtuse.com",
                    FirstName = "Dad",
                    LastName = "Wonkulah",
                    PhoneNumber = "0775060775",
                    EmailConfirmed = true
                }, "tuseTheProgrammer96!").GetAwaiter().GetResult();

                var user = _applicationDbContext.ApplicationUsers.Where(user => user.Email == "admin@iamtuse.com").FirstOrDefault();

                _userManagerService.AddToRoleAsync(user, SD.ManagerRole).GetAwaiter().GetResult();

               
            }
        }
    }
}
