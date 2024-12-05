// using System.ComponentModel.DataAnnotations;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Sephora.Data;
// using Sephora.Models;

// public static class SeedData
// {
//     public static void Initialize(IServiceProvider serviceProvider)
//     {
//         using (var context = new ApplicationDbContext(
//             serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
//         {
//             // **Adăugarea rolurilor**
//             AddRoles(context);

//             // **Adăugarea utilizatorilor**
//             AddUsers(context);

//             // **Asocierea utilizatorilor cu rolurile**
//             AssociateUsersWithRoles(context);

//             // Salvează modificările finale
//             //context.SaveChanges();
//         }
//     }

//     private static void AddRoles(ApplicationDbContext context)
//     {
//         var roles = new[]
//         {
//             new IdentityRole
//             {
//                 Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
//                 Name = "Admin",
//                 NormalizedName = "ADMIN"
//             },
//             new IdentityRole
//             {
//                 Id = "2c5e174e-3b0e-446f-86af-483d56fd7211",
//                 Name = "Editor",
//                 NormalizedName = "EDITOR"
//             },
//             new IdentityRole
//             {
//                 Id = "2c5e174e-3b0e-446f-86af-483d56fd7212",
//                 Name = "User",
//                 NormalizedName = "USER"
//             }
//         };

//         foreach (var role in roles)
//         {
//             if (!context.Roles.Any(r => r.Name == role.Name))
//             {
//                 context.Roles.Add(role);
//             }
//         }
//     }

//     private static void AddUsers(ApplicationDbContext context)
//     {
//         var hasher = new PasswordHasher<ApplicationUser>();
//         var users = new[]
//         {
//             new ApplicationUser
//             {
//                 Id = "8e445865-a24d-4543-a6c6-9443d048cdb0",
//                 UserName = "admin@test.com",
//                 Email = "admin@test.com",
//                 NormalizedEmail = "ADMIN@TEST.COM",
//                 NormalizedUserName = "ADMIN@TEST.COM",
//                 EmailConfirmed = true,
//                 PasswordHash = hasher.HashPassword(null, "Admin1!")
//             },
//             new ApplicationUser
//             {
//                 Id = "8e445865-a24d-4543-a6c6-9443d048cdb1",
//                 UserName = "editor@test.com",
//                 Email = "editor@test.com",
//                 NormalizedEmail = "EDITOR@TEST.COM",
//                 NormalizedUserName = "EDITOR@TEST.COM",
//                 EmailConfirmed = true,
//                 PasswordHash = hasher.HashPassword(null, "Editor1!")
//             },
//             new ApplicationUser
//             {
//                 Id = "8e445865-a24d-4543-a6c6-9443d048cdb2",
//                 UserName = "user@test.com",
//                 Email = "user@test.com",
//                 NormalizedEmail = "USER@TEST.COM",
//                 NormalizedUserName = "USER@TEST.COM",
//                 EmailConfirmed = true,
//                 PasswordHash = hasher.HashPassword(null, "User1!")
//             }
//         };

//         foreach (var user in users)
//         {
//             if (!context.Users.Any(u => u.UserName == user.UserName))
//             {
//                 context.Users.Add(user);
//             }
//         }
//     }

//     private static void AssociateUsersWithRoles(ApplicationDbContext context)
//     {
//         var userRoles = new[]
//         {
//             new IdentityUserRole<string>
//             {
//                 RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210", // Admin Role
//                 UserId = "8e445865-a24d-4543-a6c6-9443d048cdb0" // Admin User
//             },
//             new IdentityUserRole<string>
//             {
//                 RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7211", // Editor Role
//                 UserId = "8e445865-a24d-4543-a6c6-9443d048cdb1" // Editor User
//             },
//             new IdentityUserRole<string>
//             {
//                 RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7212", // User Role
//                 UserId = "8e445865-a24d-4543-a6c6-9443d048cdb2" // Normal User
//             }
//         };

//         foreach (var userRole in userRoles)
//         {
//             if (!context.UserRoles.Any(ur => ur.RoleId == userRole.RoleId && ur.UserId == userRole.UserId))
//             {
//                 context.UserRoles.Add(userRole);
//             }
//         }
//     }
// }