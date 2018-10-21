using System;
using System.Text;

namespace AzureStorage
{
    public class FileMetadata
    {
        public string Filename { get; set; }
        public long Size { get; set; }
        public DateTime UploadedDeate { get; set; }
        public string StoringUri { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"FileName:{Filename}");
            builder.Append(Environment.NewLine);
            builder.Append($"Size:{Size}");
            builder.Append(Environment.NewLine);
            builder.Append($"ModifiedDate:{UploadedDeate}");
            builder.Append(Environment.NewLine);
            builder.Append($"StoringUri:{StoringUri}");
            return builder.ToString();
        }
    }
}
