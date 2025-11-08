using Galaxy.AcademicMagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Galaxy.AcademicMagement.Infrastructure.Configurations.Entities.AcademicContext
{
    public class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.ToTable("Professor");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Specialization)
                .IsRequired()
                .HasMaxLength(200);

            // Configure IdentityDocument as owned entity
            builder.OwnsOne(p => p.Document, doc =>
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

            // Configure relationship with Courses
            builder.HasMany(p => p.Courses)
                .WithOne(c => c.Professor)
                .HasForeignKey(c => c.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure base entity properties
            builder.Property(p => p.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.UpdatedAt)
                .IsRequired();


        }
    }
}