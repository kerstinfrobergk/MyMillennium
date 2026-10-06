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
        private readonly IBlobStorageService _blobStorageService;
        private const long MaxFileSize = 200_000; // Represents 200 KB
        
        public ArtController(AppDbContext dbContext, IBlobStorageService blobStorageService)
        {
            _dbContext = dbContext;
            _blobStorageService = blobStorageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(
            [FromForm] ImageUploadRequest request,
            CancellationToken cancellationToken)
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

            await _blobStorageService.UploadBlobAsync(stream, blobName, cancellationToken);

            var artItem = new ArtItem()
            {
                Title = request.Title,
                Description = request.Description,
                ItemCategory = request.ItemCategory,
                BlobName = blobName
            };

            await using var transaction =
                await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            _dbContext.ArtItems.Add(artItem);
            await _dbContext.SaveChangesAsync(cancellationToken);

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
            await _dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return Ok();
        }

        [HttpGet("getImages")]
        public async Task<IActionResult> GetImagesAsync(
            CancellationToken cancellationToken)
        {
            var galleryItems = new List<ArtItemDto>();

            var galleryItemsResult = await _dbContext.ArtItems
                .OrderByDescending(x => x.Id)
                .Take(10)
                .ToListAsync(cancellationToken);

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
