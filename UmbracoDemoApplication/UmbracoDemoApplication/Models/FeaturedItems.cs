namespace UmbracoDemoApplication.Models
{
    public class FeaturedItems
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public string LinkUrl { get; set; }

        public FeaturedItems(string name, string category, string imageurl, string linkurl)
        {
            Name = name;
            Category = category;
            ImageUrl = imageurl;
            LinkUrl = linkurl;
        }
    }
}