using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthGambling.models
{
    public class db_tables : DbContext
    {
        public db_tables(DbContextOptions<db_tables> options) : base(options)
        {

        }
        public DbSet<customers> customers { get; set; } 
 
    }
}
