namespace UmbracoDemoApplication.Models
{
    public class TestimonialModel
    {
        public string Quote { get; set; }
        public string Name { get; set; }

        public TestimonialModel(string quote, string name)
        {
            Quote = quote;
            Name = name;
        }
    }
}