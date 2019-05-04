using Microsoft.EntityFrameworkCore;
using System;

namespace EFSuggestions
{
    public class Context : DbContext
    {
        public DbSet<Suggestion> Suggestions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; database=SuggestionDB; Trusted_Connection=true");
        }
    }
    public class Suggestion
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
    }
}
