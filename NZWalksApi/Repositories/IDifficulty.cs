using NZWalksApi.Models.Domain;

namespace NZWalksApi.Repositories
{
    public interface IDifficulty
    {

        Task<IEnumerable<Difficulty>> GetDifficultyListAsync();
        Task<Difficulty> GetDifficultyById(Guid id);
        Task<Difficulty> CreateDifficultyAsync(Difficulty difficulty);
        Task<Difficulty?> UpdateDifficultyAsync(Guid id, Difficulty difficulty);
        Task<Difficulty?> DeleteDifficultyAsync(Guid id);
    }
}
