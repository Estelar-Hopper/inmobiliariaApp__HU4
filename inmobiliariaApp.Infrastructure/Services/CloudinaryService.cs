using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using inmobiliariaApp.Application.Dtos;
using inmobiliariaApp.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ProductCatalog.Application.Interfaces;

namespace inmobiliariaApp.Infrastructure.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinarySettings> config)
    {
        var acc = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret
            );
        
        _cloudinary = new Cloudinary(acc);
    }
    
    public async Task<string> UploadImageAsync(UploadFileDto file)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, file.FileStream),
            Folder = "properties_imgs"
        };
        
        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.Error != null)
            throw new Exception(uploadResult.Error.Message);
        
        return uploadResult.SecureUrl.AbsoluteUri;
    }
}