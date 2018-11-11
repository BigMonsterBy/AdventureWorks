using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Models;
using System;
using System.Threading.Tasks;

namespace AzureSearch
{

    public class AzureSearchService : IAzureSearchService
    {
        private readonly string _serviceClientName;
        private readonly string _serviceClientKey;

        public AzureSearchService(string name, string key)
        {
            _serviceClientName = name;
            _serviceClientKey = key;
        }

        public async Task<DocumentSearchResult<WordDocument>> SearchDocsAsync(string searchTerm)
        {
            try
            {
                SearchParameters parameters = new SearchParameters
                {
                    Select = new[] { "metadata_storage_name, metadata_storage_path, metadata_last_modified, metadata_storage_size, metadata_word_count" }
                };

                SearchIndexClient searchIndexClient = new SearchIndexClient(_serviceClientName, "azureblob-index", new SearchCredentials(_serviceClientKey));

                return await searchIndexClient.Documents.SearchAsync<WordDocument>(searchTerm, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DocumentSearchResult<Product>> SearchProductsAsync(string searchTerm, string filter)
        {
            try
            {
                SearchParameters parameters = new SearchParameters
                {
                    Select = new[] { "*" },
                    IncludeTotalResultCount = true,
                    Facets = new[] { "Color" }
                };

                if (!string.IsNullOrEmpty(filter))
                {
                    parameters.Filter = filter;
                }

                SearchIndexClient searchIndexClient = new SearchIndexClient(_serviceClientName, "azuresql-index", new SearchCredentials(_serviceClientKey));

                return await searchIndexClient.Documents.SearchAsync<Product>(searchTerm, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}