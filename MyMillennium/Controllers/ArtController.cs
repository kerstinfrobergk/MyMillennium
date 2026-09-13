using Microsoft.AspNetCore.Mvc;
using MyMillenniumApi.Services;

namespace MyMillenniumApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtController : ControllerBase
    {
        private readonly BlobStorageService _blobStorageService;

        public ArtController(BlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadTest(IFormFile file)
        {
            using var stream = file.OpenReadStream();

            await _blobStorageService.UploadFileAsync(
                stream, file.FileName);

            return Ok();
        }
    }
}
