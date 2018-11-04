using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Models;
using System;

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

        public DocumentSearchResult<Product> SearchProducts(string searchTerm)
        {
            try
            {
                SearchParameters parameters = new SearchParameters
                {
                    Select = new[] { "*" }
                };
                SearchIndexClient searchIndexClient = new SearchIndexClient(_serviceClientName, "azuresql-index", new SearchCredentials(_serviceClientKey));

                return searchIndexClient.Documents.Search<Product>("*", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}