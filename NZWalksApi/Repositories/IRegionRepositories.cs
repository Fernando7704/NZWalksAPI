using NZWalksApi.Models.Domain;

namespace NZWalksApi.Repositories
{
    public interface IRegionRepositories
    {
        Task<List<Region>> GetAllAsync();

        Task<Region?> GetByIdAsync(Guid id);
        Task<Region> addRegionAsync(Region region);
        Task<Region?> updateAsync(Guid id, Region region);
        Task<Region?> deleteRegionAsync(Guid id);
    }
}
