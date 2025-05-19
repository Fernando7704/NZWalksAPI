using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalksApi.Data
{
    public class NZWalksAuthDbContext:IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options):base(options) 
        {         
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerID = "dba1173-7adf-4622-aa8b-96cbe468e91c";
            var writerId = "938d1d17-a9ac-472f-a539-feb50fb3e68a";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerID,
                    ConcurrencyStamp = readerID,
                    Name = "Lector",
                    NormalizedName = "Lector".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerId,
                    ConcurrencyStamp = writerId,
                    Name = "Escritor",
                    NormalizedName = "Escritor".ToUpper()

                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
