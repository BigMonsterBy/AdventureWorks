using Microsoft.Azure.Search.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureSearch
{
    public interface IAzureSearchService
    {
        Task<DocumentSearchResult<Product>> SearchProductsAsync(string searchTerm, string filter);
        Task<DocumentSearchResult<WordDocument>> SearchDocsAsync(string searchTerm);
    }
}
