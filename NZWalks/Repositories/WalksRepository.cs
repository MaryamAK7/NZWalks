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
        public async Task<IEnumerable<Walk>> GetAll()
        {
           return await NZWalksDbContext.Walks.ToListAsync(); 
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
