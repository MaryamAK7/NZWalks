using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAll();
        Task<Walk> GetAsync(Guid id); 
        Task<Walk> AddAsync (Walk item);
        Task<Walk> UpdateAsync (Guid id, Walk item);
        Task<Walk> DeleteAsync (Guid id);

    }
}
