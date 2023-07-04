using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebPhoneBook_2._0.AuthPersonApp;

namespace WebPhoneBook_2._0.ContextFolder
{
    public class PersonDbContext : IdentityDbContext<User>
    {
        public PersonDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
    }
}
