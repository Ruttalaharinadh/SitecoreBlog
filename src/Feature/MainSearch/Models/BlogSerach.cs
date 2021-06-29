using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Collections.Generic;
using System.Web;

namespace SitecoreBlog.Feature.MainSearch.Models
{
    public class SearchModel : SearchResultItem
    {
        [IndexField("_name")]
        public virtual string ItemName { get; set; }

        [IndexField("author_t")]
        public virtual string Author { get; set; }

        [IndexField("description_t")]
        public virtual string Description { get; set; } // Custom field on my template

        [IndexField("bodytitle_t")]
        public virtual string BodyTitle { get; set; } // Custom field on my template
        [IndexField("shortdescription_t")]
        public virtual string ShortDescription { set; get; }
        [IndexField("longdescription_t")]

        public virtual string LongDescription { set; get; }
    }

    public class SearchResult
    {
        public string ItemName { get; set; }
        public string BodyTitle { get; set; }
        public string Author { get; set; }
        
        public  string LongDescription { set; get; }
        public  string ShortDescription { set; get; }


    }

    /// <summary>
    /// Custom search result model for binding to front end
    /// </summary>
    public class SearchResults
    {
        public List<SearchResult> Results { get; set; }
    }



    public class Bodyresult
    {    
        public string BodyTitle { set; get; }
        public  string Id { set; get; }
        public string ShortDescription { set; get; }
        public string LongDescription { set; get; }
        public string PostedDate { set; get; }
        public string ArticleSmallImage { set; get; }
        public string ArticleLargeImage { set; get; }
        public string Tags { set; get; }
        public string Author { set; get; }


    }
   public class body
    {
        public List<Bodyresult> bodyresults { set; get; }
    }
}