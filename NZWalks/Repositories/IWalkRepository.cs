using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAll(string? filterOn = null, string? filterQuery = null,
            string? sortby = null, bool isAsc = true, int pageNumber = 1, int pageSize = 1000 );
        Task<Walk> GetAsync(Guid id); 
        Task<Walk> AddAsync (Walk item);
        Task<Walk> UpdateAsync (Guid id, Walk item);
        Task<Walk> DeleteAsync (Guid id);

    }
}
