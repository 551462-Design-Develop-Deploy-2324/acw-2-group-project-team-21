var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable authentication and authorization (if needed for role-based access)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "dashboard",
    pattern: "{controller=Home}/{action=StudentDashboard}/{id?}",
    defaults: new { controller = "Home", action = "StudentDashboard" });

app.MapControllerRoute(
    name: "supervisor-dashboard",
    pattern: "{controller=Home}/{action=SupervisorDashboard}/{id?}",
    defaults: new { controller = "Home", action = "SupervisorDashboard" });

app.MapControllerRoute(
    name: "lecturer-dashboard",
    pattern: "{controller=Home}/{action=LecturerDashboard}/{id?}",
    defaults: new { controller = "Home", action = "LecturerDashboard" });

app.Run();
