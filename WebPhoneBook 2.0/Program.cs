using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using WebPhoneBook_2._0;
using WebPhoneBook_2._0.AuthPersonApp;
using WebPhoneBook_2._0.ContextFolder;
using WebPhoneBook_2._0.Data;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IPersonData, PersonData>();
builder.Services.AddMvc();
builder.Services.AddDbContext<PersonDbContext>(options => options.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB; 
                                            DataBase = [PersonDB3]; 
                                            Trusted_connection = true;"));
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<PersonDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6; // минимальное количество знаков в пароле
    options.Lockout.MaxFailedAccessAttempts = 10; // количество попыток о блокировки
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    options.Lockout.AllowedForNewUsers = true;
});
builder.Services.ConfigureApplicationCookie(options =>
{
    // конфигурация Cookie с целью использования их для хранения авторизации
    options.Cookie.HttpOnly = true;
    //options.Cookie.Expiration = TimeSpan.FromMinutes(30);
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.SlidingExpiration = true;
});

var app = builder.Build();

 app.UseAuthentication();

using (var scope = app.Services.CreateScope())
{
    var s = scope.ServiceProvider;
    var c = s.GetRequiredService<PersonDbContext>();
    DbInitializer.Initialize(c);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Book/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}");

app.Run();

public static class DbInitializer
{
    public static void Initialize(PersonDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Persons.Any()) return;

       
        string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "People.json");
        string jsonContent = System.IO.File.ReadAllText(jsonFilePath);
        List<Person> sections = JsonConvert.DeserializeObject<List<Person>>(jsonContent);

        using (var trans = context.Database.BeginTransaction())
        {
            foreach (Person section in sections)
            {
                context.Persons.Add(section);
            }
            context.Database.ExecuteSql($"SET IDENTITY_INSERT [Persons] OFF");
            context.SaveChanges();
            context.Database.ExecuteSql($"SET IDENTITY_INSERT [Persons] ON");
            trans.Commit();
        }


    }
}