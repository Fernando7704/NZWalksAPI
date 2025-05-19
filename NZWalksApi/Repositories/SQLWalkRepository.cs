using Microsoft.EntityFrameworkCore;
using NZWalksApi.Data;
using NZWalksApi.Models.Domain;

namespace NZWalksApi.Repositories
{
    public class SQLWalkRepository : IWalksRepositories
    {
        private readonly NZWalksDbContextcs nZWalksDbContextcs;
        public SQLWalkRepository(NZWalksDbContextcs contextcs)
        {
            this.nZWalksDbContextcs = contextcs;
            
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await nZWalksDbContextcs.AddAsync(walk);
            await nZWalksDbContextcs.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var exisitingWalk = await nZWalksDbContextcs.walks.FirstOrDefaultAsync(x=>x.id==id);
            if (exisitingWalk == null) {
                return null;
            }
            nZWalksDbContextcs.walks.Remove(exisitingWalk);
            await nZWalksDbContextcs.SaveChangesAsync();
            return exisitingWalk;
        }

        public async Task<List<Walk>> GetAllAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortby = null,
            bool? ascendente=true,
            int numberPage = 1,
            int pageSize = 1000
            )
        {
            var walks=  nZWalksDbContextcs.walks.Include("difficulty").Include("region").AsQueryable();

            //filtering
            if(string.IsNullOrWhiteSpace(filterOn) ==false && string.IsNullOrWhiteSpace(filterQuery)==false)
            {
                if (filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.name.Contains(filterQuery));
                }

            }
            //sorting
            if (string.IsNullOrWhiteSpace(sortby) == false) {

                if (sortby.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = (bool)ascendente ? walks.OrderBy(x => x.name) :walks.OrderByDescending(x => x.name);
                }else if (sortby.Equals("Length",StringComparison.OrdinalIgnoreCase))
                {
                    walks = (bool)ascendente ? walks.OrderBy(x => x.lengthInKm) : walks.OrderByDescending(x => x.lengthInKm);
                }
            
            }

            //Pagination
            var skipResult = (numberPage - 1) * pageSize;
                return await walks.Skip(skipResult).Take(pageSize).ToListAsync();
            //return await nZWalksDbContextcs.walks.Include("difficulty").Include("region").ToListAsync();
            //return await nZWalksDbContextcs.walks.Include(x=>x.difficulty).Include(x=>x.region).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await nZWalksDbContextcs.walks.Include(x=>x.difficulty).Include(x=>x.region).FirstOrDefaultAsync(x =>x.id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var exisitingWalk = await nZWalksDbContextcs.walks.FirstOrDefaultAsync(x => x.id == id);
            if (exisitingWalk == null) {
                return null;
            }
            exisitingWalk.name = walk.name;
            exisitingWalk.descripcion = walk.descripcion;
            exisitingWalk.lengthInKm = walk.lengthInKm;
            exisitingWalk.walkImageUrl = walk.walkImageUrl;
            exisitingWalk.difficultyId = walk.difficultyId;
            exisitingWalk.regionId = walk.regionId;
            await nZWalksDbContextcs.SaveChangesAsync();
            return exisitingWalk;   
        }
    }
}
