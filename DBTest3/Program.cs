using AutoMapper;
using DBTest3.Areas.Identity;
using DBTest3.Config;
using DBTest3.Data;
using DBTest3.Data.Entity;
using DBTest3.Data.ViewModels;
using DBTest3.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDefaultIdentity<User>(options => { options.Password.RequireNonAlphanumeric = false; options.Password.RequireDigit = false; options.SignIn.RequireConfirmedAccount = false; })
    .AddRoles<Role>()
    .AddRoleManager<RoleManager<Role>>()
    .AddUserManager<UserManager<User>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddRazorPages();

builder.Services.AddServerSideBlazor();

builder.Services.AddTransient<IApplicationRoleService, ApplicationRoleService>();
builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();
builder.Services.AddTransient<ITicketService, TicketService>();
builder.Services.AddTransient<ICompanyService, CompanyService>();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddTransient<IProjectService, ProjectService>();

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();

var app = builder.Build();

Config.RegisterMappings(typeof(ErrorViewModel).GetType().Assembly);

using (var scope = app.Services.CreateScope())
{
    var roles = scope.ServiceProvider.GetService<IApplicationRoleService>();
    var user = scope.ServiceProvider.GetService<IApplicationUserService>();
    var ticket = scope.ServiceProvider.GetService<ITicketService>();
    var company = scope.ServiceProvider.GetService<ICompanyService>();

    if (!company.hasAny())
        await company.CrateCompanyAsync("Admin");

    await roles.initRoles();

    await user.createUserAdmin();

    await ticket.initTicketStatuses();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
