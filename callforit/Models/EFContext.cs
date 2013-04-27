using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace callforit.Models
{
    public class EFContext : DbContext
    {
        public EFContext() : base("improvingwa-test_db")
        {
        }

        public DbSet<Conference> Conferences { get; set; }
    }
}