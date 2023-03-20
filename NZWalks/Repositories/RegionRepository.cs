using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;
        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid Id)
        {
            return await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id== Id);
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = new Guid();
           await nZWalksDbContext.Regions.AddAsync(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id== id);
            if (region == null)
            {
                return null;
            }
            nZWalksDbContext.Regions.Remove(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var dbRegion = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(dbRegion== null)
            {
                return null;
            }
            dbRegion.Code = region.Code;
            dbRegion.Name = region.Name;
            dbRegion.Lat= region.Lat;
            dbRegion.Long= region.Long; 
            dbRegion.Population= region.Population;

            await nZWalksDbContext.SaveChangesAsync();
            return dbRegion;
        }
    }
}
