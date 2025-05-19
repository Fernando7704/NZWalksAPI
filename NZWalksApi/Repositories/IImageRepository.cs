using NZWalksApi.Models.Domain;
using System.Net;

namespace NZWalksApi.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
