using Microsoft.EntityFrameworkCore;
using NZWalksApi.Data;
using NZWalksApi.Models.Domain;

namespace NZWalksApi.Repositories
{
    public class SQLDifficultiesRespository : IDifficulty
    {
        public readonly NZWalksDbContextcs _nzWalks;
        public SQLDifficultiesRespository(NZWalksDbContextcs nZWalksDbContextcs)
        {
            this._nzWalks = nZWalksDbContextcs;
        }

        public async Task<Difficulty> CreateDifficultyAsync(Difficulty difficulty)
        {
            await _nzWalks.difficulties.AddAsync(difficulty);
            await _nzWalks.SaveChangesAsync();
            return difficulty;
        }

        public async Task<Difficulty?> DeleteDifficultyAsync(Guid id)
        {
            var difficul= await _nzWalks.difficulties.FirstOrDefaultAsync(x=> x.id == id);
            if (difficul == null)
            {
                return null;
            }
            _nzWalks.difficulties.Remove(difficul);
            _nzWalks.SaveChangesAsync();
            return difficul;
        }

        public Task<Difficulty?> GetDifficultyById(Guid id)
        {
            return _nzWalks.difficulties.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<IEnumerable<Difficulty>> GetDifficultyListAsync()
        {
            return  _nzWalks.difficulties.ToList();
            
        }

        public Task<Difficulty?> UpdateDifficultyAsync(Guid id, Difficulty difficulty)
        {
           var existing = _nzWalks.difficulties.FirstOrDefaultAsync(x=> x.id == id);
            if (existing == null)
            {
                return null;
            }
            existing.Result.Name = difficulty.Name;
            _nzWalks.SaveChangesAsync();
            return existing;
        }
    }
}
