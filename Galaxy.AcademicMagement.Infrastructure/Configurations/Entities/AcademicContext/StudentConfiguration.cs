using Galaxy.AcademicMagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Galaxy.AcademicMagement.Infrastructure.Configurations.Entities.AcademicContext
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(200);

            // Configure IdentityDocument as owned entity
            builder.OwnsOne(s => s.Document, doc =>
            {
                doc.Property(d => d.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("DocumentType");
                    
                doc.Property(d => d.Number)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("DocumentNumber");
            });

            // Configure relationship with Enrollments
            builder.HasMany(s => s.Enrollments)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure base entity properties
            builder.Property(s => s.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(s => s.CreatedAt)
                .IsRequired();

            builder.Property(s => s.UpdatedAt)
                .IsRequired();

    
        }
    }
}