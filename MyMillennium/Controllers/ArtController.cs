using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMillennium.Contracts.Messages;
using MyMillennium.Data.DataAccess;
using MyMillenniumApi.DTOs;
using MyMillennium.Data.Entities;
using MyMillenniumApi.Services;
using System.Text.Json;

namespace MyMillenniumApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly BlobStorageService _blobStorageService;
        private readonly ServiceBusService _serviceBusService;

        private const long MaxFileSize = 200_000; // Represents 200 KB
        
        public ArtController(AppDbContext dbContext, BlobStorageService blobStorageService, ServiceBusService serviceBusService)
        {
            _dbContext = dbContext;
            _blobStorageService = blobStorageService;
            _serviceBusService = serviceBusService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequest request)
        {
            if (request.File.Length > MaxFileSize)
            {
                return BadRequest(
                    new ErrorResponse($"Image too large. Max size is {MaxFileSize/1000} KB."));
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(request.File.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest(
                    new ErrorResponse("Filetype needs to be JPG, JPEG or PNG."));
            }

            using var stream = request.File.OpenReadStream();

            var filePathExtension = Path.GetExtension(request.File.FileName);
            var blobName = $"{Guid.NewGuid()}{filePathExtension}";

            await _blobStorageService.UploadBlobAsync(stream, blobName);

            var artItem = new ArtItem()
            {
                Title = request.Title,
                Description = request.Description,
                ItemCategory = request.ItemCategory,
                BlobName = blobName
            };

            await using var transaction =
                await _dbContext.Database.BeginTransactionAsync();

            _dbContext.ArtItems.Add(artItem);
            await _dbContext.SaveChangesAsync();

            var message = new ProcessArtImage(
                artItem.Id,
                blobName);

            var outboxMessage = new OutboxMessage()
            {
                MessageType = nameof(ProcessArtImage),
                Payload = JsonSerializer.Serialize(message),
                CreatedAtUtc = DateTime.UtcNow
            };

            _dbContext.OutboxMessages.Add(outboxMessage);
            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();

            return Ok();
        }

        [HttpGet("getImages")]
        public async Task<IActionResult> GetImagesAsync()
        {
            var galleryItems = new List<ArtItemDto>();

            var galleryItemsResult = await _dbContext.ArtItems
                .Where(x => x.BlobName != null &&
                            (x.ItemCategory == Category.Inspiration || x.ItemCategory == Category.ProfilePicture))  //TODO: Consider what filtering makes sense
                .OrderByDescending(x => x.Id)
                .Take(10)
                .ToListAsync();

            foreach (var galleryItem in galleryItemsResult)
            {
                var artItemDto = new ArtItemDto()
                {
                    Id = galleryItem.Id,
                    ImageUrl = _blobStorageService.GetBlobSasUrl(galleryItem.BlobName),
                    Title = galleryItem.Title,
                    Description = galleryItem.Description,
                    ThumbnailUrl = !string.IsNullOrWhiteSpace(galleryItem.ThumbnailBlobName)
                        ? _blobStorageService.GetBlobSasUrl(galleryItem.ThumbnailBlobName)
                        : null
                };

                galleryItems.Add(artItemDto);
            }

            return Ok(galleryItems);
        }
    }
}
