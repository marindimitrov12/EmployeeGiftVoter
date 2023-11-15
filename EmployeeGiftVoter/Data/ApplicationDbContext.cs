using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
     : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Event>()
                .HasOne(x => x.Initiator)
                .WithMany(x => x.Events)
                .HasForeignKey(x => x.InitiatorId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<EventResult>()
               .HasOne(x => x.Event)
               .WithMany(x => x.Results)
               .HasForeignKey(x => x.EventId)
               .OnDelete(DeleteBehavior.ClientSetNull);







        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventResult> EventResults { get; set; }


    }
}
