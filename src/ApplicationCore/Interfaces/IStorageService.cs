using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IStorageService
    {
        Task<Uri> UploadFilesAsync(string containerName, string folder, IEnumerable<string> files);

        Task<bool> ExistsContainerAsync(string containerName);
    }
}
