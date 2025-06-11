using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ucode.Core.Models;

namespace Ucode.Api.Data.Mappings
{
    public class EnrollmentMapping : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.ToTable("Enrollment");            

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EnrollmentNumber)
            .IsRequired()
            .HasMaxLength(8)
            .HasColumnType("VARCHAR");

            builder.Property(x => x.Status)
           .IsRequired(true)
           .HasColumnType("SMALLINT");

            builder.Property(x => x.CreatedAt)
                .IsRequired(true);

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);

            builder.Property(x => x.StudentId)             
                .IsRequired(true);                        

            builder.Property(x => x.CourseId)
                .IsRequired(true);                       

            builder.Property(x => x.UserId)
                .IsRequired(true)
                .HasColumnType("VARCHAR")
                .HasMaxLength(160);

            // Configuração das FKs sem cascata na exclusão
            builder.HasOne(x => x.Student)
                .WithMany()
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Course)
                .WithMany()
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
