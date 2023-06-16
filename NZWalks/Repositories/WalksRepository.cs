using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class WalksRepository : IWalkRepository
    {
        public readonly NZWalksDbContext NZWalksDbContext;
        public WalksRepository(NZWalksDbContext context)
        {
            NZWalksDbContext = context;
        }
        public async Task<IEnumerable<Walk>> GetAll(string? filterOn = null, string? filterQuery = null,
            string? sortby = null, bool isAsc = true,
            int pageNumber = 1,int pageSize = 1000 )
        {   var walks = NZWalksDbContext.Walks.Include(x => x.Region).Include(x => x.WalkDifficulty).AsQueryable(); 
            if(string.IsNullOrWhiteSpace(filterOn) == false || string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }
            if (string.IsNullOrWhiteSpace(sortby) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAsc ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
            }
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync(); 
        } 
        public async Task<Walk> GetAsync(Guid id)
        {
           return await NZWalksDbContext.Walks.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Walk> AddAsync(Walk walk)
        {
            walk.Id = new Guid();
           await NZWalksDbContext.Walks.AddAsync(walk);
          await  NZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk item)
        {
            var walkDb = await NZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if(walkDb == null)
            {
                return null;
            }
                walkDb.Name = item.Name;
                walkDb.WalkDifficultyId = item.WalkDifficultyId;
                walkDb.RegionId = item.RegionId;
                walkDb.Length = item.Length;
            await NZWalksDbContext.SaveChangesAsync();
            return item;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walkDb = await NZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if( walkDb == null)
            {
                return null;
            }
            NZWalksDbContext.Walks.Remove(walkDb);
           await NZWalksDbContext.SaveChangesAsync();
            return walkDb;
        }
    }
}
