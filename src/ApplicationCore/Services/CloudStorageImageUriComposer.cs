using ApplicationCore.Interfaces;
using Microsoft.WindowsAzure.Storage;

namespace ApplicationCore.Services
{
    public class CloudStorageImageUriComposer : IUriComposer
    {
        private CloudStorageAccount storageAccount;

        public CloudStorageImageUriComposer(string connectionString)
        {
            storageAccount = CloudStorageAccount.Parse(connectionString);
        }

        public string ComposePicUri(string uriTemplate)
        {
            var blobEndpointUri = storageAccount.BlobEndpoint.AbsoluteUri;
            if (!blobEndpointUri.EndsWith("/")) blobEndpointUri += "/";

            return uriTemplate.Replace("http://catalogbaseurltobereplaced/images/", blobEndpointUri);
        }
    }
}
