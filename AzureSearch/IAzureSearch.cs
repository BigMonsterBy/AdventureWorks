using Microsoft.Azure.Search.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureSearch
{
    public interface IAzureSearchService
    {
        DocumentSearchResult<Product> SearchProducts(string searchTerm);
    }
}
