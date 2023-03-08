using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IRegionRepository
    {
       Task<IEnumerable<Region>> GetAllAsync();
    }
}
