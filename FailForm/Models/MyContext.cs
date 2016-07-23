using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FailForm.Models
{
    public class MyContext : DbContext
    {
        public MyContext() : base() { }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<InfoStorage> infoStore { get; set; }
    }
}