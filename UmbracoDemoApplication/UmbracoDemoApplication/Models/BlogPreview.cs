namespace UmbracoDemoApplication.Models
{
    public class BlogPreview
    {
        public string Name { get; set; }
        public string Introduction { get; set; }
        public string ImageUrl { get; set; }
        public string LinkUrl { get; set; }

        public BlogPreview(string name, string introduction, string imageurl, string linkurl)
        {
            Name = name;
            Introduction = introduction;
            ImageUrl = imageurl;
            LinkUrl = linkurl;
        }
    }
}