using AutoService.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Web.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var context = services.GetRequiredService<ApplicationDbContext>();

            // -------------------------
            // 1. Crează roluri
            // -------------------------
            string[] roles = { "Admin", "Employee", "Client" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // -------------------------
            // 2. Crează Admin
            // -------------------------
            var adminEmail = "admin@autoservice.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Name = "Administrator"
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // -------------------------
            // 3. Crează Client
            // -------------------------
            var clientEmail = "client@autoservice.com";
            var clientUser = await userManager.FindByEmailAsync(clientEmail);
            if (clientUser == null)
            {
                clientUser = new ApplicationUser
                {
                    UserName = clientEmail,
                    Email = clientEmail,
                    Name = "Client"
                };

                var result = await userManager.CreateAsync(clientUser, "Client123!");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                await userManager.AddToRoleAsync(clientUser, "Client");
            }

            // -------------------------
            // 4. Creează Vehicles
            // -------------------------
            if (!context.Vehicles.Any())
            {
                var vehicle1 = new Vehicle
                {
                    Brand = "Toyota",
                    Model = "Corolla",
                    Year = 2020,
                    LicensePlate = "B123ABC",
                    UserId = clientUser.Id
                };

                var vehicle2 = new Vehicle
                {
                    Brand = "Ford",
                    Model = "Focus",
                    Year = 2018,
                    LicensePlate = "B456DEF",
                    UserId = clientUser.Id
                };

                context.Vehicles.AddRange(vehicle1, vehicle2);
                await context.SaveChangesAsync(); // Salvează după fiecare tip de entitate
            }

            // -------------------------
            // 5. Creează Services
            // -------------------------
            if (!context.Services.Any())
            {
                var service1 = new Service
                {
                    Name = "Oil Change",
                    Description = "Change engine oil",
                    Price = 50
                };

                var service2 = new Service
                {
                    Name = "Brake Check",
                    Description = "Check brakes",
                    Price = 80
                };

                context.Services.AddRange(service1, service2);
                await context.SaveChangesAsync();
            }

            // -------------------------
            // 6. Creează Appointments
            // -------------------------
            if (!context.Appointments.Any())
            {
                var vehicle = await context.Vehicles.FirstAsync();
                var appointment1 = new Appointment
                {
                    VehicleId = vehicle.Id,
                    UserId = clientUser.Id,
                    Date = DateTime.Now.AddDays(1),
                    Status = "Scheduled"
                };

                context.Appointments.Add(appointment1);
                await context.SaveChangesAsync();
            }

            // -------------------------
            // 7. Creează ServiceRecords
            // -------------------------
            if (!context.ServiceRecords.Any())
            {
                var appointment = await context.Appointments.FirstAsync();
                var service = await context.Services.FirstAsync();

                var record = new ServiceRecord
                {
                    AppointmentId = appointment.Id,
                    ServiceId = service.Id,
                    Notes = "Performed successfully"
                };

                context.ServiceRecords.Add(record);
                await context.SaveChangesAsync();
            }

            // -------------------------
            // 8. Creează Review
            // -------------------------
            if (!context.Reviews.Any())
            {
                var appointment = await context.Appointments.FirstAsync();

                var review = new Review
                {
                    AppointmentId = appointment.Id,
                    Rating = 5,
                    Comment = "Great service!"
                };

                context.Reviews.Add(review);
                await context.SaveChangesAsync();
            }
        }
    }
}
