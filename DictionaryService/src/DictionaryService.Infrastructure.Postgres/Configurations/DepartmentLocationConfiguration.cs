using DictionaryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DictionaryService.Infrastructure.Postgres.Configurations;

internal sealed class DepartmentLocationConfiguration : IEntityTypeConfiguration<DepartmentLocation>
{
    public void Configure(EntityTypeBuilder<DepartmentLocation> builder)
    {
        builder.ToTable("department_locations");

        builder.HasKey(departmentLocation => new
            {
                departmentLocation.DepartmentId,
                departmentLocation.LocationId
            })
            .HasName("pk_department_locations");

        builder.Property(departmentLocation => departmentLocation.DepartmentId)
            .HasColumnName("department_id");

        builder.Property(departmentLocation => departmentLocation.LocationId)
            .HasColumnName("location_id");

        builder.HasOne(departmentLocation => departmentLocation.Department)
            .WithMany(department => department.Locations)
            .HasForeignKey(departmentLocation => departmentLocation.DepartmentId)
            .HasConstraintName("fk_department_locations_department_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(departmentLocation => departmentLocation.Location)
            .WithMany()
            .HasForeignKey(departmentLocation => departmentLocation.LocationId)
            .HasConstraintName("fk_department_locations_location_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(departmentLocation => departmentLocation.LocationId)
            .HasDatabaseName("ix_department_locations_location_id");
    }
}
