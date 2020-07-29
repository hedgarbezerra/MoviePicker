using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OurMovies.MoviePicker.MVC.Models
{
    public class DefaultResponse<T> where T: class
    {
        public List<T> data { get; set; }
        public string message { get; set; }
        public bool success { get; set; }
    }
}