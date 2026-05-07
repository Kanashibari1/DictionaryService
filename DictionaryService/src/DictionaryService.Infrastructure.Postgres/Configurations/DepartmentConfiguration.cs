using DictionaryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DictionaryService.Infrastructure.Postgres.Configurations;

internal sealed class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");

        builder.HasKey(department => department.Id)
            .HasName("pk_departments");

        builder.Property(department => department.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(department => department.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .HasConversion(ValueObjectConverters.NonEmptyString)
            .IsRequired();

        builder.Property(department => department.Slug)
            .HasColumnName("slug")
            .HasMaxLength(100)
            .HasConversion(ValueObjectConverters.Slug)
            .IsRequired();

        builder.Property(department => department.HierarchyPath)
            .HasColumnName("hierarchy_path")
            .HasMaxLength(2048)
            .HasConversion(ValueObjectConverters.HierarchyPath)
            .IsRequired();

        builder.Property(department => department.ParentId)
            .HasColumnName("parent_id");

        builder.HasIndex(department => department.Slug)
            .IsUnique()
            .HasDatabaseName("ux_departments_slug");

        builder.HasIndex(department => department.ParentId)
            .HasDatabaseName("ix_departments_parent_id");

        builder.HasIndex(department => department.HierarchyPath)
            .HasDatabaseName("ix_departments_hierarchy_path");

        builder.HasOne(department => department.Parent)
            .WithMany(department => department.Children)
            .HasForeignKey(department => department.ParentId)
            .HasConstraintName("fk_departments_parent_id")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(department => department.Locations)
            .WithOne(departmentLocation => departmentLocation.Department)
            .HasForeignKey(departmentLocation => departmentLocation.DepartmentId)
            .HasConstraintName("fk_department_locations_department_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(department => department.Post)
            .WithOne(departmentPosition => departmentPosition.Department)
            .HasForeignKey(departmentPosition => departmentPosition.DepartmentId)
            .HasConstraintName("fk_department_positions_department_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(department => department.Children)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(department => department.Locations)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(department => department.Post)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
