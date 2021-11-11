using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KnowBetter_WebApp.Models;

namespace KnowBetter_WebApp.Data
{
    public class KnowBetter_WebAppContext : DbContext
    {
        public KnowBetter_WebAppContext (DbContextOptions<KnowBetter_WebAppContext> options)
            : base(options)
        {
        }

        public DbSet<KnowBetter_WebApp.Models.User> User { get; set; }
    }
}
