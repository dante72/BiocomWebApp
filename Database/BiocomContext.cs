using BiocomWebApp.Database.Entity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BiocomWebApp.Database
{
    public class BiocomContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<User> Users { get; set; }
        public DbSet<Diagnostic> Diagnostics { get; set; }
        public DbSet<Diet> Diets { get; set; }
        public DbSet<DietPart> DietParts { get; set; }
        public DbSet<Substance> Substances { get; set; }
        public DbSet<SubstanceType> SubstanceTypes { get; set; }
        public DbSet<SupplementPart> SumplementParts { get; set; }
        public DbSet<Supplement> Supplements { get; set; }
        public DbSet<HealthyDiet> HealtyDiets { get; set; }
        public BiocomContext(IConfiguration configuration)
        {
            _configuration = configuration;

            if (Database.EnsureCreated())
            {
                AddDefaultUsers();
                AddDefaultSubstances();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Substance>()
                .HasIndex("Name")
                .IsUnique();

            modelBuilder.Entity<SubstanceType>()
                .HasIndex("Name")
                .IsUnique();

            modelBuilder.Entity<Supplement>()
                .HasIndex("Name")
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);
        }

        private void AddDefaultUsers()
        {
            Users.Add(new User()
            {
                FirstName = "Boris",
                LastName = "Johnson",
                Age = 100
            });
            SaveChanges();
        }

        private void AddDefaultSubstances()
        {
            var vitamin = new SubstanceType() { Name = "Vitamin" };
            var macroElement = new SubstanceType() { Name = "MacroElement" };
            var microElement = new SubstanceType() { Name = "MicroElement" };
            var mineral = new SubstanceType() { Name = "Mineral" };
            
            SubstanceTypes.AddRange(
                vitamin,
                macroElement,
                microElement,
                mineral
                );
            SaveChanges();

            Substances.AddRange(
                new Substance("Vitamin A", 900e-6, null, vitamin),
                new Substance("Vitamin B1", 1.5e-3, null, vitamin),
                new Substance("Vitamin C", 100e-3, null, vitamin),
                new Substance("Proteins", 86, 300, macroElement),
                new Substance("Fats", 45, 88, macroElement),
                new Substance("Сalcium", 1, null, macroElement, mineral),
                new Substance("Magnesium", 420e-3, 4000e-3, macroElement, mineral),
                new Substance("Iodine", 150e-6, 1000e-6, microElement, mineral)
            );
            SaveChanges();
        }
            
    }
}
