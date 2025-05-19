using NZWalksApi.Models.Domain;

namespace NZWalksApi.Repositories
{
    public interface IWalksRepositories
    {
        Task<Walk>CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy=null,
            bool? ascendente=true,
            int numberPage = 1,
            int pageSize = 1000);
        Task<Walk> GetByIdAsync(Guid id);
        Task<Walk?> UpdateAsync(Guid id,Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
