using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CodeChallenge.Entities
{
    public class CodeChallengeContext : DbContext
    {

        public virtual DbSet<ShortUrl> ShortUrls { get; set; }
        public virtual DbSet<Stat> Stats { get; set; }
    }
}