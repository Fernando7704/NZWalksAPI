using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksApi.Models.Domain;
using NZWalksApi.Models.DTO;
using NZWalksApi.Repositories;
using System.Xml.Serialization;

namespace NZWalksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        public readonly IImageRepository _ImageRepository;
        public ImagesController(IImageRepository imageRepository)
        {
            this._ImageRepository = imageRepository;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateFileUpload(imageUploadRequestDto);
            if (ModelState.IsValid)
            {
                ///COnvert DTO to Domain Model

                var imagenDomainModel = new Image
                {
                    File = imageUploadRequestDto.File,
                    FileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName),
                    FileSizeInBytes = imageUploadRequestDto.File.Length,
                    FileName = imageUploadRequestDto.File.FileName,
                    FileDescription = imageUploadRequestDto.FileDescription
                    
                };
                //Upload
                await _ImageRepository.Upload(imagenDomainModel);
                return Ok(imagenDomainModel);
            }
            return BadRequest(ModelState);

        }
        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if(!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDto.FileName))==false)
            {
                ModelState.AddModelError("File", "Unsoppoted file extension");
            }
            if(imageUploadRequestDto.File.Length>10485760)
            {
                ModelState.AddModelError("File", "El tamaño es mayor que 10MB, Por favor sube un archivo con tamaño menor a 10MB");
            }
        }
    }
}
