using Microsoft.EntityFrameworkCore;
using NZWalks.Models.Domain;

namespace NZWalks.Data
{
    public class NZWalksDbContext:DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options):base(options)
        {
                
        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }
        public DbSet<Image> Images { get; set; }

        // haydi eza bdi zid data 3l database awal mna fadye b3ml ba3da migration
        // (Add migration "Migname" w Update-Database 7ata ynzedu
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    var regions = new List<Region>
        //    {
        //        new Region
        //        {
        //            Id = new Guid(),
        //            Name = "123",
        //            Code = "123" 
        //        },
        //        new Region
        //        {
        //            Id = new Guid(),
        //            Name = "123",
        //            Code = "123"
        //        },
        //    };
        //    modelBuilder.Entity<Region>().HasData(regions);
        //}
    }
}
