using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwinnyAPI.Models
{
    public class AuthorDTO
    {
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string TFN { get; set; }
    }
}