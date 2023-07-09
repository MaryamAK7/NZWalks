using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IImageRepository
    {
        public Task<Image> Upload(Image image);
    }
}
