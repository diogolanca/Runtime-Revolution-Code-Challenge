using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeChallenge.Models
{
    public class Url
    {
        [Required]
        public string FullUrl { get; set; }

        public string ShortUrl { get; set; }

        public string CustomSegment { get; set; }
    }
}