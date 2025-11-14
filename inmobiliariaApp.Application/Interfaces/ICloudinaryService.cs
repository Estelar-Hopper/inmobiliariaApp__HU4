using inmobiliariaApp.Application.Dtos;

namespace ProductCatalog.Application.Interfaces;

public interface ICloudinaryService
{
    Task<string> UploadImageAsync(UploadFileDto file);
    Task DeleteImageAsync(string imageUrl); 
}