using Microsoft.EntityFrameworkCore;
using CourseManagementAPI.Models;

namespace CourseManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<InstructorProfile> InstructorProfiles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite key for Enrollment
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.UserId, e.CourseId });

            // User -> InstructorProfile (one-to-one)
            modelBuilder.Entity<User>()
                .HasOne(u => u.InstructorProfile)
                .WithOne(p => p.User)
                .HasForeignKey<InstructorProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User (Instructor) -> Courses (one-to-many)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(u => u.CoursesTeaching)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            // User (Student) -> Enrollments (one-to-many)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Course -> Enrollments (one-to-many)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Optional useful constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}