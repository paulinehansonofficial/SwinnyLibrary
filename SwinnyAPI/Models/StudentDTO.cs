using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwinnyAPI.Models
{
    public class StudentDTO
    {
        public string StudentID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int Mobile { get; set; }
    }
}