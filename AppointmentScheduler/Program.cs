using AppointmentScheduler.Middleware;
using AppointmentScheduler.Services;
using AppointmentScheduler.Services.ISevices;
using Business.Repository;
using Business.Repository.IRepository;
using DataAccess;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddIdentity<AppUser, IdentityRole>( o=> { o.Password.RequireNonAlphanumeric= false; }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IDBInitializer, DBInitializer>();
builder.Services.AddScoped<RepositoryService>();

builder.Configuration
    .AddEnvironmentVariables();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var scope = app.Services.CreateScope();
var dbInitializer = scope.ServiceProvider.GetRequiredService<IDBInitializer>();
dbInitializer.InitializeDB();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=login}/{id?}");

app.Run();
