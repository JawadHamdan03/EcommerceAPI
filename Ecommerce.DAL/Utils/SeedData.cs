using Ecommerce.DAL.Data;
using Ecommerce.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Utils
{
    public class SeedData : ISeedData
    {
        private readonly AppDbcontext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public SeedData(AppDbcontext _context,RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this._context = _context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

       

       public async Task SeedDataModelsAsync()
        {
            if((await _context.Database.GetPendingMigrationsAsync()).Any())
               await _context.Database.MigrateAsync();
            
            if(!await _context.Categories.AnyAsync())
            {
               await _context.Categories.AddRangeAsync(new List<Models.Category>()
                {
                    new Models.Category(){ Name="Electronics",Description="New and Old"},
                    new Models.Category(){ Name="Clothes",Description="New and Old"},
                    new Models.Category(){ Name="Books",Description="New and Old"},
                    new Models.Category(){ Name="Home Appliances",Description="New and Old"},
                    new Models.Category(){ Name="Toys",Description="New and Old"},
                });
                
            }


            if(!await _context.Brands.AnyAsync())
            {
               await _context.Brands.AddRangeAsync(
                    new Models.Brand(){ Name="Samsung"},
                    new Models.Brand(){ Name="Apple"},
                    new Models.Brand(){ Name="Microsoft"},
                    new Models.Brand(){ Name="Sony"}

                    );
            }

           await _context.SaveChangesAsync();

        }


        public async Task IdentitySeedDataAsync()
        {
            
            foreach (var role in new[] { "Admin", "SuperAdmin", "Customer" })
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var r = await roleManager.CreateAsync(new IdentityRole(role));
                    if (!r.Succeeded)
                        throw new Exception($"Create role '{role}' failed: {string.Join(", ", r.Errors.Select(e => e.Description))}");
                }
            }

           
            var users = new[]
            {
        new { FullName="Jawad Hamdan", Email="jawad@gmail.com",  Phone="0592163158", UserName="JawadHamdan",  City="Nablus",   Role="Admin",      Password="Jawad123$" },
        new { FullName="Ahmad Hamdan", Email="ahmad@gmail.com",  Phone="0594823158", UserName="AhmadHamdan", City="Hebron",   Role="SuperAdmin", Password="P@ssw0rd!Sup3r" },
        new { FullName="Omar Hamdan",  Email="omar@gmail.com",   Phone="0597263158", UserName="OmarHamdan",  City="Tulkarem", Role="Customer",   Password="P@ssw0rd!Cust1" }
    };

            foreach (var u in users)
            {
                var user = await userManager.FindByEmailAsync(u.Email);

                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        FullName = u.FullName,
                        Email = u.Email,
                        UserName = u.UserName,
                        PhoneNumber = u.Phone,
                        City = u.City,
                        EmailConfirmed = true
                    };

                    var createRes = await userManager.CreateAsync(user, u.Password);
                    if (!createRes.Succeeded)
                        throw new Exception($"Create user '{u.Email}' failed: {string.Join(", ", createRes.Errors.Select(e => e.Description))}");
                }
                else
                {
                
                    var changed = false;
                    if (user.FullName != u.FullName) { user.FullName = u.FullName; changed = true; }
                    if (user.UserName != u.UserName) { user.UserName = u.UserName; changed = true; }
                    if (user.PhoneNumber != u.Phone) { user.PhoneNumber = u.Phone; changed = true; }
                    if (user.City != u.City) { user.City = u.City; changed = true; }
                    if (!user.EmailConfirmed) { user.EmailConfirmed = true; changed = true; }

                    if (changed)
                    {
                        var updateRes = await userManager.UpdateAsync(user); 
                        if (!updateRes.Succeeded)
                            throw new Exception($"Update user '{u.Email}' failed: {string.Join(", ", updateRes.Errors.Select(e => e.Description))}");
                    }
                }

                if (!await userManager.IsInRoleAsync(user, u.Role))
                {
                    var addRoleRes = await userManager.AddToRoleAsync(user, u.Role);
                    if (!addRoleRes.Succeeded)
                        throw new Exception($"Add '{u.Email}' to role '{u.Role}' failed: {string.Join(", ", addRoleRes.Errors.Select(e => e.Description))}");
                }
            }

            
        }



    }
}
