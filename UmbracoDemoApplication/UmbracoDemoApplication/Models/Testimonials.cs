using System;
using System.Collections.Generic;

namespace UmbracoDemoApplication.Models
{
    public class Testimonials
    {
        public string Title { get; set; }
        public string Introduction { get; set; }
        public List<TestimonialModel> Testimonial { get; set; }
        public bool HasTestimonials { get { return Testimonial != null && Testimonial.Count > 0; } }
        public String ColumnClass
        {
            get
            {
                switch (Testimonial.Count)
                {
                    case 1:
                        return "col-md-12";
                    case 2:
                        return "col-md-6";
                    case 3:
                        return "col-md-4";
                    case 4:
                        return "col-md-3";
                    default:
                        return "col-md-4";
                }
            }
        }
        public Testimonials(string title, string introduction, List<TestimonialModel> testimonial)
        {
            Title = title;
            Introduction = introduction;
            Testimonial = testimonial;
        }
    }
}