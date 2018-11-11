using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorks.Web.Models;
using AzureSearch;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IAzureSearchService _azureSearchService;

        public SearchController(IAzureSearchService azureSearchService)
        {
            _azureSearchService = azureSearchService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SearchProducts(ProductSearchModel searchModel)
        {
            var searchTerm = searchModel.SearchTerm;
            var filter = string.Empty;

            if (!string.IsNullOrEmpty(searchModel.FacetName))
            {
                switch (searchModel.FacetType)
                {
                    case "Value":
                        filter = $"{searchModel.FacetName} eq '{searchModel.FacetValue}'";
                        break;
                    default:
                        throw new NotImplementedException("other facet types not supported now.");
                }
            }

            var model = await _azureSearchService.SearchProductsAsync(searchTerm, filter);

            return PartialView(model: model);
        }

        [HttpGet]
        public async Task<IActionResult> SearchDocuments(string searchTerm)
        {
            var model = await _azureSearchService.SearchDocsAsync(searchTerm);

            return PartialView(model: model);
        }
    }
}