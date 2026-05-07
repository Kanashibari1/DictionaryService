using DictionaryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DictionaryService.Infrastructure.Postgres.Configurations;

internal sealed class DepartmentPositionConfiguration : IEntityTypeConfiguration<DepartmentPosition>
{
    public void Configure(EntityTypeBuilder<DepartmentPosition> builder)
    {
        builder.ToTable("department_positions");

        builder.HasKey(departmentPosition => new
            {
                departmentPosition.DepartmentId,
                departmentPosition.PostId
            })
            .HasName("pk_department_positions");

        builder.Property(departmentPosition => departmentPosition.DepartmentId)
            .HasColumnName("department_id");

        builder.Property(departmentPosition => departmentPosition.PostId)
            .HasColumnName("post_id");

        builder.HasOne(departmentPosition => departmentPosition.Department)
            .WithMany(department => department.Post)
            .HasForeignKey(departmentPosition => departmentPosition.DepartmentId)
            .HasConstraintName("fk_department_positions_department_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(departmentPosition => departmentPosition.Post)
            .WithMany(post => post.Departments)
            .HasForeignKey(departmentPosition => departmentPosition.PostId)
            .HasConstraintName("fk_department_positions_post_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(departmentPosition => departmentPosition.PostId)
            .HasDatabaseName("ix_department_positions_post_id");
    }
}
