using Microsoft.AspNetCore.Mvc;
using MyMillenniumApi.Data;
using MyMillenniumApi.Services;

namespace MyMillenniumApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly BlobStorageService _blobStorageService;
        private readonly ServiceBusService _serviceBusService;

        public ArtController(AppDbContext dbContext, BlobStorageService blobStorageService, ServiceBusService serviceBusService)
        {
            _dbContext = dbContext;
            _blobStorageService = blobStorageService;
            _serviceBusService = serviceBusService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(
            [FromForm] IFormFile file,
            [FromForm] string title,
            [FromForm] string description,
            [FromForm] Category itemCategory)
        {
            using var stream = file.OpenReadStream();

            var filePathExtension = Path.GetExtension(file.FileName);
            var blobName = $"{Guid.NewGuid()}{filePathExtension}";

            await _blobStorageService.UploadFileAsync(stream, blobName);

            var artItem = new ArtItem()
            {
                Title = title,
                Description = description,
                ItemCategory = itemCategory,
                BlobName = blobName
            };

            _dbContext.ArtItems.Add(artItem);
            await _dbContext.SaveChangesAsync();

            var message = new ProcessArtImage(
                artItem.Id,
                blobName);

            await _serviceBusService.SendProcessArtImageAsync(message);

            return Ok();
        }
    }
}
