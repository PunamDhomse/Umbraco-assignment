using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web;
using UmbracoDemoApplication.Models;
using System.Linq;

namespace UmbracoDemoApplication.Controllers
{
    public class SiteLayoutController : SurfaceController
    {
        private string PartialViewPath(string name)
        {
            return $"~/Views/Partials/SiteLayout/{name}.cshtml";
        }

        [Obsolete]
        public ActionResult RenderHeader()
        {
            List<NavigationListItem> nav = GetObjectFromCache<List<NavigationListItem>>("mainNav", 0, GetNavigationModelFromDatabase);
            return PartialView(PartialViewPath("_Header"), nav);
        }

        public ActionResult RenderIntro()
        {
            return PartialView(PartialViewPath("_Intro"));
        }

        public ActionResult RenderTitleControls()
        {
            return PartialView(PartialViewPath("_TitleControls"));
        }

        public ActionResult RenderFooter()
        {
            return PartialView(PartialViewPath("_Footer"));
        }

        /// <summary>
        /// Finds the home page and gets the navigation structure based on it and it's children
        /// </summary>
        /// <returns>A List of NavigationListItems, representing the structure of the site.</returns>
        [Obsolete]
        private List<NavigationListItem> GetNavigationModelFromDatabase()
        {

            IPublishedContent homePage = CurrentPage.AncestorOrSelf(1).DescendantsOrSelf().Where(x => x.DocumentTypeAlias == "home").FirstOrDefault();
            List<NavigationListItem> nav = new List<NavigationListItem>();
            nav.Add(new NavigationListItem(new NavigationLink(homePage.Url, homePage.Name)));
            nav.AddRange(GetChildNavigationList(homePage));
            return nav;
        }

        /// <summary>
        /// Loops through the child pages of a given page and their children to get the structure of the site.
        /// </summary>
        /// <param name="page">The parent page which you want the child structure for</param>
        /// <returns>A List of NavigationListItems, representing the structure of the pages below a page.</returns>
        [Obsolete]
        private List<NavigationListItem> GetChildNavigationList(IPublishedContent page)
        {
            List<NavigationListItem> listItems = null;
            var childPages = page.Children.Where("Visible").Where(x => x.Level <= 2).Where(x => !x.HasProperty("excludeFromTopNavigation") || (x.GetPropertyValue<bool>("excludeFromTopNavigation") && !x.GetPropertyValue<bool>("excludeFromTopNavigation")));
            if (childPages != null && childPages.Any() && childPages.Count() > 0)
            {
                listItems = new List<NavigationListItem>();
                foreach (var childPage in childPages)
                {
                    NavigationListItem listItem = new NavigationListItem(new NavigationLink(childPage.Url, childPage.Name));
                    listItem.Items = GetChildNavigationList(childPage);
                    listItems.Add(listItem);
                }
            }
            return listItems;
        }
        private static T GetObjectFromCache<T>(string cacheItemName, int cacheTimeInMinutes, Func<T> objectSettingFunction)
        {
            ObjectCache cache = MemoryCache.Default;
            var cachedObject = (T)cache[cacheItemName];
            if (cachedObject == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(cacheTimeInMinutes);
                cachedObject = objectSettingFunction();
                cache.Set(cacheItemName, cachedObject, policy);
            }
            return cachedObject;
        }
    }
}