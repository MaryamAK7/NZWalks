using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly NZWalksDbContext context;
        private IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LocalImageRepository(
            NZWalksDbContext context,
            IWebHostEnvironment webHostenv, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            env = webHostenv;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(env.ContentRootPath, "Images",image.File.FileName);
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //http://localhost:1234/images/image.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.File.FileName}";
            image.FilePath = urlFilePath;
            await context.Images.AddAsync(image);
            await context.SaveChangesAsync();

            return image;
        }
    }
}
