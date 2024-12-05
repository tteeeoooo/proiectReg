using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Store.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurare baza de date
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(9, 0, 1))
    ));





builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; 
    options.SignIn.RequireConfirmedEmail = false; 
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


//builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();



// Adaugă autorizare și suport pentru Razor Pages
builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


// Înregistrarea serviciilor
//builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
//builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();

var app = builder.Build();





using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Definim rolurile
    string[] roles = new[] { "UtilizatorNeinregistrat", "Inregistrat", "Colaborator", "Administrator" };

    foreach (var role in roles)
    {
        // Dacă rolul nu există, îl creăm
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Adaugăm un utilizator Administrator implicit (opțional)
    var adminEmail = "admin@example.com";
    var adminPassword = "Admin123!";
    
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        await userManager.CreateAsync(adminUser, adminPassword);
        await userManager.AddToRoleAsync(adminUser, "Administrator");
    }
}





// Pipeline middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Autentificare
app.UseAuthorization();  // Autorizare

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Dacă folosești Razor Pages

app.Run();









// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Store.Data;

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseMySql(
//         builder.Configuration.GetConnectionString("DefaultConnection"),
//         new MySqlServerVersion(new Version(8, 0, 31)) // Versiunea MySQL
//     ));

// builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();












// // builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
// //     .AddEntityFrameworkStores<ApplicationDbContext>();
// // builder.Services.AddControllersWithViews();






// builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//     .AddEntityFrameworkStores<ApplicationDbContext>()
//     .AddDefaultTokenProviders();
// //builder.Services.AddAuthentication(); 
// builder.Services.AddAuthorization();  

// builder.Services.AddControllersWithViews();
// builder.Services.AddRazorPages();

// var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// {
//     var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//     var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

//     string[] roles = new[] { "UtilizatorNeinregistrat", "Inregistrat", "Colaborator", "Administrator" };

//     foreach (var role in roles)
//     {
//         if (!await roleManager.RoleExistsAsync(role))
//         {
//             await roleManager.CreateAsync(new IdentityRole(role));
//         }
//     }

//     // Opțional: Creează un utilizator administrator implicit
//     var adminEmail = "admin@example.com";
//     var adminPassword = "Admin123!";

//     var adminUser = await userManager.FindByEmailAsync(adminEmail);
//     if (adminUser == null)
//     {
//         adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
//         await userManager.CreateAsync(adminUser, adminPassword);
//         await userManager.AddToRoleAsync(adminUser, "Administrator");
//     }
// }



















// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseMigrationsEndPoint();
// }
// else
// {
//     app.UseExceptionHandler("/Home/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }

// app.UseHttpsRedirection();
// app.UseStaticFiles();

// app.UseRouting();

// app.UseAuthentication();
// app.UseAuthorization();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");


// app.MapRazorPages();

// app.Run();
