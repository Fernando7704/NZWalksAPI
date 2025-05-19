using NZWalksApi.Data;
using NZWalksApi.Models.Domain;

namespace NZWalksApi.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        public readonly IWebHostEnvironment _webHost;
        public readonly IHttpContextAccessor _accessor;
        public readonly NZWalksDbContextcs _walks;

        public LocalImageRepository(
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            NZWalksDbContextcs nZWalksDb)
        {
            this._webHost = webHostEnvironment;
            this._accessor = httpContextAccessor;
            this._walks = nZWalksDb;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(_webHost.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");
            //Upload Image in Local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //https://localhost:1234/Images/imag.jpg
            var urlFilePath = 
                $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}{_accessor.HttpContext.Request.PathBase}:/images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            //Add Image to DB

            await _walks.image.AddAsync(image);
            await _walks.SaveChangesAsync();
            return image;
        }
    }
}
