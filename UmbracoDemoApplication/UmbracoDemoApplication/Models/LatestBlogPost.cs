namespace UmbracoDemoApplication.Models
{
    public class LatestBlogPost
    {
        public string Title { get; set; }
        public string Introduction { get; set; }

        public LatestBlogPost(string title, string introduction)
        {
            Title = title;
            Introduction = introduction;
        }
    }
}