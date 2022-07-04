using Student_Affairs.Models;
using Microsoft.EntityFrameworkCore;
namespace Student_Affairs.Data
{
    public class StudentAffairsContext : DbContext
    {
        public StudentAffairsContext(DbContextOptions<StudentAffairsContext> options) : base(options)
        {
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentSubject>()
                .HasKey(s => new { s.StudentID, s.SubjectID });
        }
    }
}
