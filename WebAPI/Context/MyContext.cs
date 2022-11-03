using Microsoft.EntityFrameworkCore;
using WebAPI.Modes;

namespace WebAPI.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> context) : base(context)
        {

        }

        public DbSet<Division> Divisions {get; set;}
        public DbSet<Departement> Departements { get; set; }

    }
}
