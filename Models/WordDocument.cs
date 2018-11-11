using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class WordDocument
    {
        [JsonProperty("metadata_storage_name")]
        public string StorageName { get; set; }

        [JsonProperty("metadata_storage_path")]
        public string StoragePath { get; set; }

        [JsonProperty("metadata_last_modified")]
        public DateTime LastModified { get; set; }

        [JsonProperty("metadata_word_count")]
        public int WordCount { get; set; }

        [JsonProperty("metadata_storage_size")]
        public long Size { get; set; }
    }
}
