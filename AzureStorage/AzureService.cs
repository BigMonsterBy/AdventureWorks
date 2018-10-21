using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage
{
    public class AzureService : IAzureService
    {
        const string ContainerName = "awappblob";
        const string QueueName = "awappqueue";
        private readonly CloudStorageAccount _cloudStorageAccount;
        public AzureService(string name, string key)
        {
            var creds = new StorageCredentials(name, key);
            _cloudStorageAccount = new CloudStorageAccount(creds, true);
            //_cloudStorageAccount = CloudStorageAccount.DevelopmentStorageAccount;
        }

        public async Task<string> AddBlobAsync(Stream stream, string name)
        {
            var blobClient = _cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(ContainerName);
            await container.CreateIfNotExistsAsync();

            var newBlob = container.GetAppendBlobReference(name);
            newBlob.Properties.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            if (await newBlob.ExistsAsync())
            {
                throw new Exception($"Blob {name} exists.");
            }
            else
            {
                await newBlob.CreateOrReplaceAsync();
            }

            await newBlob.AppendFromStreamAsync(stream);

            return newBlob.StorageUri.PrimaryUri.AbsoluteUri;
        }

        public async Task AddToQueueAsync(FileMetadata fileMetadata)
        {
            var queueClient = _cloudStorageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference(QueueName);
            await queue.CreateIfNotExistsAsync();

            var message = new CloudQueueMessage(fileMetadata.ToString());
            await queue.AddMessageAsync(message);
        }
    }
}
