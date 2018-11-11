using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorks.Web.Models
{
    public class ProductSearchModel
    {
        public string SearchTerm { get; set; }
        public string FacetName { get; set; }
        public string FacetType { get; set; }
        public string FacetValue { get; set; }
        public string FacetFrom { get; set; }
        public string FacetTo { get; set; }
    }
}
