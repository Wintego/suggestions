using Microsoft.EntityFrameworkCore;
using System;

namespace WebApplication5.Models
{
    public class Context : DbContext
    {
        public DbSet<Suggestion> Suggestions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; database=SuggestionDB; Trusted_Connection=true");
        }
    }
}
