using AdminBaker.Entities.Configuration;
using AdminBaker.Services.Interfaces;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
namespace AdminBaker.Services.Implementations;

public class AzureBlobStorageUploader : IFileUploader
{
    private readonly AppConfig _appConfig;
    private readonly ILogger<AzureBlobStorageUploader> _logger;

    public AzureBlobStorageUploader(IOptions<AppConfig> options, ILogger<AzureBlobStorageUploader> logger)
    {
        _logger = logger;
        _appConfig = options.Value;
    }

    public async Task<string> UploadFileAsync(string? base64String, string? fileName)
    {
        if (string.IsNullOrWhiteSpace(base64String) || string.IsNullOrWhiteSpace(fileName))
            return string.Empty;

        try
        {
            var client = new BlobServiceClient(_appConfig.StorageConfiguration.Path);
            var container = client.GetBlobContainerClient("pasteles");

            var blob = container.GetBlobClient(fileName);

            await using var stream = new MemoryStream(Convert.FromBase64String(base64String));
            await blob.UploadAsync(stream, overwrite: true);

            _logger.LogInformation("Se subio la imagen a Azure Blob Storage");

            return $"{_appConfig.StorageConfiguration.PublicUrl}{fileName}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al subir archivos a Azure Blob Storage {Message}", ex.Message);
            return string.Empty;
        }
    }
}