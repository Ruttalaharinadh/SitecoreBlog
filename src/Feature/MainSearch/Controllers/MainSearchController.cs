
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using SitecoreBlog.Feature.MainSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace SitecoreBlog.Feature.MainSearch.Controllers
{
    public class MainSearchController : Controller
    {
        private object typemerger;

        // GET: MainSearch
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {

           List<Bodyresult> x = new List<Bodyresult>();

            //List<String> items = new List<String>();
        
        Sitecore.Data.Items.Item y  =Sitecore.Context.Database.GetItem("{F0EEF137-A59F-4EF3-9A2E-53E136A4EA08}");


            Sitecore.Data.Items.Item j = Sitecore.Context.Database.GetItem("{F0EEF137-A59F-4EF3-9A2E-53E136A4EA08}");
            //var T = y.Fields.Concat(j);
            var a = y.Fields.Union(j);
           
            //  string str = item.Fields["Title"].Value;
            return View("~/Views/SitecoreBlog/Search.cshtml", y);
        }


        public ActionResult DoSearch(string searchText)
        {
            var myResults = new SearchResults
            {
                Results = new List<SearchResult>()
            };
            var searchIndex = ContentSearchManager.GetIndex("sitecore_web_index"); // Get the search index
            var searchPredicate = GetSearchPredicate(searchText); // Build the search predicate
            using (var searchContext = searchIndex.CreateSearchContext()) // Get a context of the search index
            {
                //Select * from Sitecore_web_index Where Author="searchText" OR Description="searchText" OR Title="searchText"
                //var searchResults = searchContext.GetQueryable<SearchModel>().Where(searchPredicate); // Search the index for items which match the predicate
                var searchResults = searchContext.GetQueryable<SearchModel>()
                    .Where(x => x.Author.Contains(searchText) || x.BodyTitle.Contains(searchText) || x.LongDescription.Contains(searchText) || x.ShortDescription.Contains(searchText));   //LINQ query

                var fullResults = searchResults.GetResults();

                // This is better and will get paged results - page 1 with 10 results per page
                //var pagedResults = searchResults.Page(1, 10).GetResults();
                foreach (var hit in fullResults.Hits)
                {
                    myResults.Results.Add(new SearchResult
                    {
                        ShortDescription = hit.Document.ShortDescription,
                        BodyTitle = hit.Document.BodyTitle,
                        LongDescription=hit.Document.LongDescription,
                        ItemName = hit.Document.ItemName,
                        Author = hit.Document.Author
                    });
                }
                return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = myResults };
            }
        }

        /// <summary>
        /// Search logic
        /// </summary>
        /// <param name="searchText">Search term</param>
        /// <returns>Search predicate object</returns>
        public static Expression<Func<SearchModel, bool>> GetSearchPredicate(string searchText)
        {
            var predicate = PredicateBuilder.True<SearchModel>(); // Items which meet the predicate
                                                                  // Search the whole phrase - LIKE
                                                                  // predicate = predicate.Or(x => x.DispalyName.Like(searchText)).Boost(1.2f);
                                                                  // predicate = predicate.Or(x => x.Description.Like(searchText)).Boost(1.2f);
                                                                  // predicate = predicate.Or(x => x.Title.Like(searchText)).Boost(1.2f);
                                                                  // Search the whole phrase - CONTAINS
            predicate = predicate.Or(x => x.Author.Contains(searchText)); // .Boost(2.0f);
            predicate = predicate.Or(x => x.Description.Contains(searchText)); // .Boost(2.0f);
            predicate = predicate.Or(x => x.BodyTitle.Contains(searchText)); // .Boost(2.0f);
            predicate = predicate.Or(x => x.LongDescription.Contains(searchText)); // .Boost(2.0f);
            predicate = predicate.Or(x => x.ShortDescription.Contains(searchText)); // .Boost(2.0f);

            //Where Author="searchText" OR Description="searchText" OR Title="searchText"
            return predicate;
        }

    }

}
