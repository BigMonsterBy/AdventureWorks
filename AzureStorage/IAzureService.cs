using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage
{
    public interface IAzureService
    {
        Task<string> AddBlobAsync(Stream stream, string name);

        Task AddToQueueAsync(FileMetadata fileMetadata);
    }
}
