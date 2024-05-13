using API.Dtos;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        /// <summary>
        /// Uploads a photo to the server.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/photo/uploads
        ///     FormData:
        ///         file: {FILE_DATA}
        /// </remarks>
        /// <param name="file">The photo file to upload.</param>
        /// <returns>The uploaded photo information.</returns>
        [HttpPost("uploads")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<PhotoUploadDto>> AddPhoto([FromForm ] IFormFile file)
        {
            var response = await _photoService.AddPhoto(file);

            return Ok(response);

        } 
    }
}
