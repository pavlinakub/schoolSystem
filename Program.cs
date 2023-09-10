using CoreMVC.Models;
using CoreMVC.Services;
using Microsoft.AspNetCore.Identity;        //username:pavlina007 password:Abcd1234
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();    //vytvori nam to instance

#region pridani CONNECTION STRING LOCAL DB 2.MOZNOSTI
//1.moznost  LOKALNI DATABAZE
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDBConecction"));
//});

//2.moznost je citlivejsi na zapis  LOKALNI DATABAZE
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration["ConnectionStrings: SchoolDBConecction"]);
//});


//3.moznost AZURE DATABAZE
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureDbConnection"));
});

//4.moznost webhosting SOMEE.COM
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SomeeDBConnection"));
//});
#endregion

//pridani dbContextu s identityDbContext 
builder.Services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
#region PRIDANI StudentService injection do programu
builder.Services.AddScoped<StudentsService>();

builder.Services.AddScoped<SubjectsService>();
builder.Services.AddScoped<GradeService>();

#endregion


//regulace hesla users/create  -zustava v platnost cislice,mala a velka pismena
builder.Services.Configure<IdentityOptions>(option => { option.Password.RequireNonAlphanumeric = false; option.Password.RequiredLength = 8; });

//cookie+jak dlouho cookie(prihlaseni)vydrzi
builder.Services.ConfigureApplicationCookie(options => { options.Cookie.Name = ".AspNetCore.Identity.Application"; options.ExpireTimeSpan = TimeSpan.FromMinutes(20);options.SlidingExpiration = true; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  //prihlaseni uzivatele je vzdy 1.
app.UseAuthorization();     //pristup uzivatele je vzdy 2.

app.MapControllerRoute(   //co bude domovska stranka aplikace
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
