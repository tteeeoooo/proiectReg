using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Store.Controllers
{
    [Authorize(Roles = "Administrator")] 
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("Utilizatorul nu a fost găsit.");
            }

            if (!await _roleManager.RoleExistsAsync(role))
            {
                return BadRequest("Rolul specificat nu există.");
            }

            var result = await _userManager.AddToRoleAsync(user, role);
            if (result.Succeeded)
            {
                return Ok($"Utilizatorul {user.UserName} a fost adăugat în rolul {role}.");
            }

            return BadRequest(result.Errors);
        }

        // [HttpPost("create-user")]
        // public async Task<IActionResult> CreateUser()
        // {
        //     var user = new IdentityUser
        //     {
        //         UserName = "testuser@example.com",
        //         Email = "testuser@example.com",
        //         EmailConfirmed = true
        //     };

        //     var result = await _userManager.CreateAsync(user, "Password123!");
        //     if (result.Succeeded)
        //     {
        //         return Ok("Utilizator creat cu succes.");
        //     }
        //     else
        //     {
        //         return BadRequest(result.Errors);
        //     }
        // }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser()
        {
            var user = new IdentityUser
            {
                UserName = "testuser@example.com",
                Email = "testuser@example.com",
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, "Password123!");
            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "Administrator");
                if (roleResult.Succeeded)
                {
                    return Ok($"Utilizatorul {user.UserName} a fost creat și adăugat în rolul Administrator.");
                }
                else
                {
                    return BadRequest(roleResult.Errors);
                }
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        
    }
}


/*
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Store.Data; // Înlocuiește cu namespace-ul tău pentru ApplicationDbContext
using Store.Models;
using Sephora.Models; // Înlocuiește cu namespace-ul tău pentru AdminProfile

namespace Store.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(string email, string password, string firstName, string lastName, string department)
        {
            // 1. Creează utilizatorul în IdentityUser
            var user = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "Administrator");
                if (!roleResult.Succeeded)
                {
                    return BadRequest(roleResult.Errors);
                }

                var adminProfile = new AdminProfile
                {
                    Id = user.Id,
                    FirstName = firstName,
                    LastName = lastName,
                    Department = department,
                    CreatedAt = DateTime.UtcNow
                };

                _context.AdminProfiles.Add(adminProfile);
                await _context.SaveChangesAsync();

                return Ok($"Utilizatorul {user.UserName} a fost creat și adăugat în rolul Administrator.");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet("get-admin-profile")]
        public async Task<IActionResult> GetAdminProfile(string userId)
        {
            var profile = await _context.AdminProfiles
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == Id);

            if (profile == null)
            {
                return NotFound("Profilul nu a fost găsit.");
            }

            return Ok(profile);
        }
    }
}*/