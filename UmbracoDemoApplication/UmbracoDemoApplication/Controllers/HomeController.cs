using Archetype.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using UmbracoDemoApplication.Models;

namespace UmbracoDemoApplication.Controllers
{
    public class HomeController : SurfaceController
    {
        private string PartialViewPath(string name)
        {
            return $"~/Views/Partials/Home/{name}.cshtml";
        }

        private const int MAXIMUM_TESTIMONIALS = 4;

        public ActionResult RenderFeatured()
        {
            List<FeaturedItems> model = new List<FeaturedItems>();
            IPublishedContent homePage = CurrentPage.AncestorOrSelf(1).DescendantsOrSelf().Where(x => x.DocumentTypeAlias == "home").FirstOrDefault();
            ArchetypeModel featuredModel = homePage.GetPropertyValue<ArchetypeModel>("featuredItems");

            //foreach(ArchetypeFieldsetModel fieldset in featuredModel)
            //{
            //    int imageId = fieldset.GetValue<int>("image");
            //    var mediaItem = Umbraco.Media(imageId);
            //    string imageUrl = mediaItem.Url;

            //    //int pageId = fieldset.GetValue<int>("page");
            //    //IPublishedContent linkdToPage = Umbraco.TypedContent(pageId);
            //    //string linkUrl = linkdToPage.Url;

            //    model.Add(new FeaturedItems(fieldset.GetValue<string>("name"), fieldset.GetValue<string>("category"), imageUrl, fieldset.GetValue<string>("page")));
            //}

            foreach (var item in featuredModel)
            {
                //FeaturedItems items = new FeaturedItems();
                string name = item.GetValue<string>("name");

                string category = item.GetValue<string>("category");
                //item.Category = item.GetValue<string>("category");

                int imageId = item.GetValue<IPublishedContent>("image").Id;
                var mediaItem = Umbraco.Media(imageId);
                string imageUrl = mediaItem.Url;

                int pageId = item.GetValue<IPublishedContent>("page").Id;
                IPublishedContent linkedToPage = Umbraco.TypedContent(pageId);
                string linkUrl = linkedToPage.Url;

                model.Add(new FeaturedItems(name, category, imageUrl, linkUrl));
            }

            return PartialView("~/Views/Partials/Home/_Featured.cshtml", model);
        }

        public ActionResult RenderServices()
        {
            return PartialView(PartialViewPath("_Services"));
        }

        public ActionResult RenderBlog()
        {
            IPublishedContent homePage = CurrentPage.AncestorOrSelf("home");
            string title = homePage.GetPropertyValue<string>("latestBlogPostTitle");
            string introduction = homePage.GetPropertyValue("latestBlogPostTitle").ToString();

            LatestBlogPost model = new LatestBlogPost(title, introduction);
            return PartialView(PartialViewPath("_Blog"), model);
        }
        public ActionResult RenderTestimonials()
        {
            IPublishedContent homePage = CurrentPage.AncestorOrSelf("home");

            string title = homePage.GetPropertyValue<string>("testimonialTitle");
            string introduction = homePage.GetPropertyValue("testimonialsIntroduction").ToString();

            List<TestimonialModel> testimonial = new List<TestimonialModel>();

            ArchetypeModel testmonialLists = homePage.GetPropertyValue<ArchetypeModel>("testimonialList");

            if (testmonialLists != null)
            {
                foreach (ArchetypeFieldsetModel testimonials in testmonialLists.Take(MAXIMUM_TESTIMONIALS))
                {
                    string name = testimonials.GetValue<string>("name");
                    string quote = testimonials.GetValue<string>("quote");

                    testimonial.Add(new TestimonialModel(quote, name));
                }
            }
            Testimonials model = new Testimonials(title, introduction, testimonial);
            return PartialView(PartialViewPath("_Testimonials"), model);
        }

    }
}