using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SimplePOS.Data;
using SimplePOS.Services;
using SimplePOS.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// register POS service -- no repository approach for ease.
builder.Services.AddScoped<IPosService, PosService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// unified middleware for error handling 
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pos}/{action=Index}/{id?}");
app.Run();