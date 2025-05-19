using NZWalksApi.Models.Domain;

namespace NZWalksApi.Repositories
{
    public class InMemoryRegions //: IRegionRepositories
    {
        public async Task<List<Region>> GetAllAsync()
        {
            return new List<Region>(){
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Code = "Alguna región",
                    Name = "Nombre de la region"
                }
            };
        }
    }
}
