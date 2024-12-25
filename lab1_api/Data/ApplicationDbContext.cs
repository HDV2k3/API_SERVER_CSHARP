using lab1_api.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace lab1_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
     
    }
}

