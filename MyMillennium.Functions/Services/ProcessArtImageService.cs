using Microsoft.EntityFrameworkCore;
using MyMillennium.Contracts.Messages;
using MyMillennium.Data.DataAccess;

namespace MyMillennium.Functions.Services
{
    public class ProcessArtImageService
    {
        private readonly AppDbContext _dbContext;

        public ProcessArtImageService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ProcessArtItemAsync(ProcessArtImage processArtImage)
        {
            var artItem = await _dbContext.ArtItems
                .Where(x => x.Id == processArtImage.ArtItemId)
                .FirstOrDefaultAsync();

            if (artItem == null)
            {
                return;
            }

            // TODO: Retrieve the blob represented by processArtImage.BlobName

            // TODO: Generate thumbnail
        }
    }
}
