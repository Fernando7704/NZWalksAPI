using Microsoft.EntityFrameworkCore;
using NZWalksApi.Data;
using NZWalksApi.Models.Domain;

namespace NZWalksApi.Repositories
{
    public class SQLRegionRepository : IRegionRepositories
    {
        private readonly NZWalksDbContextcs context;
        public SQLRegionRepository(NZWalksDbContextcs dbContextcs)
        {
               this.context = dbContextcs;
        }

        public async Task<Region?> addRegionAsync(Region region)
        {
            await context.regions.AddAsync(region);
            await context.SaveChangesAsync();
            return region;
        }

        public async Task<Region> deleteRegionAsync(Guid id)
        {
            var regions = await context.regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regions == null) {
                return null;
            }
            context.regions.Remove(regions);
            await context.SaveChangesAsync();  
            return regions;
        }

        public  async Task<List<Region>> GetAllAsync()
        {
           return  await context.regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await context.regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> updateAsync(Guid id, Region region)
        {
            var existingRegion= await context.regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImagenUrl = region.RegionImagenUrl;

            await context.SaveChangesAsync();
            return existingRegion;
        }
    }
}
