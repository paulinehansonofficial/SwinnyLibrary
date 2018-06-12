using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwinnyAPI.Models
{
    public class BookDTO
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string YearPublished { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
    }
}