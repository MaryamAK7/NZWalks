using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [Route("Upload")]
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] ImageUpload request)
        {
            ValidateFileUpload(request);
            if(ModelState.IsValid)
            {
                var imageDomain = new Image
                {
                    File = request.File,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                    FileSizeInBytes = request.File.Length,
                    FileExtension = Path.GetExtension(request.File.FileName)
                };
                await imageRepository.Upload(imageDomain);
                return Ok(imageDomain);
            };
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(ImageUpload request)
        {
            var allowedExtensions = new string[]  {  ".jpg",".png", ".jped"};
            if(!allowedExtensions.Contains(Path.GetExtension(request.File.FileName))) {
                ModelState.AddModelError("file", "Unsupported file extension.");
            }
            //file larger than 10 mb
            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size exceeded 10Mb.");
            }
        }
    }
}
