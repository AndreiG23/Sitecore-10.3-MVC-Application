using AndreiHelix.Feature.BlogSite.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace AndreiHelix.Controllers
{
    public class HomeController : Controller
    {
        private List<Sitecore.Data.Items.Item> GetItems()
        {
            List<Sitecore.Data.Items.Item> items = new List<Sitecore.Data.Items.Item>();
            var searchIndex = ContentSearchManager.GetIndex("sitecore_web_index");

            using (var searchContext = searchIndex.CreateSearchContext())
            {
                var searchResults = searchContext.GetQueryable<SearchModel>()
                    .Where(x => x.TemplateID == "3c0bf5498617459296a6ca11e1a70f2a");

                var fullResults = searchResults.GetResults();

                foreach (var hit in fullResults.Hits)
                {
                    items.Add(Sitecore.Context.Database.GetItem(hit.Document.ArticleID));
                }
            }
            return items.Where(i => i != null).OrderByDescending(x => x.Fields["CreationDate"].Value).ToList();
        }
        public ActionResult Index()
        {
            return View("~/Views/BlogSite/Index.cshtml", GetItems());
        }

        public ActionResult DoSearch(string searchString)
        {
            return View("~/Views/BlogSite/Index.cshtml", GetItems().Where(x=> x.Fields["Title"].Value.Contains(searchString)));
        }

        public ActionResult ReadOn(string id)
        {
            var item = Sitecore.Context.Database.GetItem(id);
            return View("~/Views/BlogSite/Article.cshtml", item);
        }
    }
}