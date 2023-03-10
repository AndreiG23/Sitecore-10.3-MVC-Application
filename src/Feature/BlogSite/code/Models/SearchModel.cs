using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AndreiHelix.Feature.BlogSite.Models
{
    public class SearchModel : SearchResultItem
    {
        [IndexField("articleid_t")]
        public virtual string ArticleID { get; set; } 

        [IndexField("_template")]
        public virtual string TemplateID { get; set; }

        [IndexField("title_te")]
        public virtual string Title { get; set; }
    }

    public class SearchResult
    {
        public string ArtilceId { get; set; }
        public string TemplateID { get; set; }
        public string Title { get; set; }
    }

    /// <summary>
    /// Custom search result model for binding to front end
    /// </summary>
    public class SearchResults
    {
        public List<SearchResult> Results { get; set; }
    }
}
