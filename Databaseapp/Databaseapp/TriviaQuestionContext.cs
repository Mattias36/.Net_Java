using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Gui")]
internal class TriviaDbContext : DbContext
{
    public DbSet<TriviaQuestionDb> Questions { get; set; }
    public DbSet<AnswerDb> Answers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=trivia.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relacja 1 do wielu między TriviaQuestionDb a AnswerDb
        modelBuilder.Entity<AnswerDb>()
            .HasOne(a => a.TriviaQuestion)
            .WithMany(q => q.Answers)
            .HasForeignKey(a => a.TriviaQuestionDbId);
    }
}
