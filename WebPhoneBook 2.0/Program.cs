using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using WebPhoneBook_2._0;
using WebPhoneBook_2._0.AuthPersonApp;
using WebPhoneBook_2._0.ContextFolder;
using WebPhoneBook_2._0.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IPersonData, PersonData>();
builder.Services.AddDbContext<PersonDbContext>(options => options.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB; 
                                            DataBase = [PersonDB]; 
                                            Trusted_connection = true;"));
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<PersonDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var s = scope.ServiceProvider;
    var c = s.GetRequiredService<PersonContext>();
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
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}");

app.Run();

public static class DbInitializer
{
    public static void Initialize(PersonContext context)
    {
        context.Database.EnsureCreated();

        if (context.Persons.Any()) return;

        var sections = new List<Person>()
        {
                new Person(1,"Марк1","sd", "sdf","sfdgh", "sdg","+7" ),
                new Person(2,"Марк2","sd", "sdf","sfdgh", "sdg","+7" ),
                new Person(3,"Марк3","sd", "sdf","sfdgh", "sdg","+7" ),
            };
        using (var trans = context.Database.BeginTransaction())
        {
            foreach (var section in sections)
            {
                context.Persons.Add(section);
            }
            context.Database.ExecuteSql($"SET IDENTITY_INSERT [PersonDb].[Persons] ON");
            context.SaveChanges();
            context.Database.ExecuteSql($"SET IDENTITY_INSERT [PersonDb].[Persons] OFF");
            trans.Commit();
        }


    }
}