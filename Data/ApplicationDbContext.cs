﻿using Finshark_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Finshark_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions options): base(options) // Base means passing the
                                                                             // into actual dbcontext class that we are inheriting form
        {
            
        }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Stock> stocks { get; set; }
    }
}