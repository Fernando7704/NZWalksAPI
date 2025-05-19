using Microsoft.EntityFrameworkCore;
using NZWalksApi.Models.Domain;

namespace NZWalksApi.Data
{
    public class NZWalksDbContextcs:DbContext
    {
        public NZWalksDbContextcs(DbContextOptions<NZWalksDbContextcs> dbContextOptions):base(dbContextOptions) 
        {
            
        }
        public DbSet <Difficulty> difficulties { get; set; }
        public DbSet<Region> regions { get; set; }
        public DbSet<Walk> walks { get; set; }
        public DbSet<Image> image { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Sembradio de información Dificultades
            var difficulties = new List<Difficulty>()
            {
                new Difficulty() { 
                    id = Guid.Parse("752f441b-3268-4729-845a-02df3c0e7e13"),
                    Name = "Facil"
                },
                new Difficulty()
                {
                    id = Guid.Parse("55a87c60-33b4-4bcf-96a8-76c644dd7e20"),
                    Name = "Normal"
                },
                new Difficulty()
                {
                    id = Guid.Parse("284a9d17-9e07-4796-a0c8-199cb2ebd637"),
                    Name = "Dificil"
                }
            };

            //Sembrar datos a la BD
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //llenando el sembradio de datos para la tabla regions

            var regions = new List<Region>() {
                new Region() {
                    Id = Guid.Parse("8f4eacfc-eaab-4cde-b87d-2b4544b8a84a"),
                    Name = "Bay of Plenty",
                    Code = "BOP",
                    RegionImagenUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fes.pngtree.com%2Ffree-backgrounds-photos%2Ffotos-anchas-pictures&psig=AOvVaw18mVrfLuAspgyKVvaKJG9A&ust=1745962585637000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCPDtstPX-4wDFQAAAAAdAAAAABAE"
                },
                new Region() {
                    Id = Guid.Parse("470b646a-063e-4a94-8346-f50135212f6d"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImagenUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fes.pngtree.com%2Ffree-backgrounds-photos%2Ffotos-anchas-pictures&psig=AOvVaw18mVrfLuAspgyKVvaKJG9A&ust=1745962585637000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCPDtstPX-4wDFQAAAAAdAAAAABAE"
                },
                 new Region() {
                    Id = Guid.Parse("e5973695-f958-41d3-8b68-8ec2d89cb159"),
                    Name = "Nelson",
                    Code = "MSN",
                    RegionImagenUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fes.pngtree.com%2Ffree-backgrounds-photos%2Ffotos-anchas-pictures&psig=AOvVaw18mVrfLuAspgyKVvaKJG9A&ust=1745962585637000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCPDtstPX-4wDFQAAAAAdAAAAABAE"
                },
                 new Region() {
                    Id = Guid.Parse("fc6d6315-4fff-41b7-8a3b-53d0224a477d"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImagenUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fes.pngtree.com%2Ffree-backgrounds-photos%2Ffotos-anchas-pictures&psig=AOvVaw18mVrfLuAspgyKVvaKJG9A&ust=1745962585637000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCPDtstPX-4wDFQAAAAAdAAAAABAE"
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
