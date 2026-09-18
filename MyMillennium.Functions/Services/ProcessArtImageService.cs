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
            //var artItemToUpdate = _dbContext.ArtItems


        }
    }
}
