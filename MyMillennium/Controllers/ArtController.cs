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
    }
}
