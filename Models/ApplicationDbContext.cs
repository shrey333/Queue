using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queue.Models
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QueueModel>().HasKey(q => new { q.UserId, q.MerchantId});
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<QueueModel> queueModels { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Merchant> merchants { get; set; }
    }
}
