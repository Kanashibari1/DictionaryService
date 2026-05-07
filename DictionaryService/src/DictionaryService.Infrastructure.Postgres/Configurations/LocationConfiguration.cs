using DictionaryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DictionaryService.Infrastructure.Postgres.Configurations;

internal sealed class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("locations");

        builder.HasKey(location => location.Id)
            .HasName("pk_locations");

        builder.Property(location => location.Id)
            .HasColumnName("id");

        builder.Property(location => location.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .HasConversion(ValueObjectConverters.NonEmptyString)
            .IsRequired();

        builder.OwnsOne(location => location.Address, address =>
        {
            address.Property(value => value.Country)
                .HasColumnName("country")
                .HasMaxLength(100)
                .IsRequired();

            address.Property(value => value.City)
                .HasColumnName("city")
                .HasMaxLength(100)
                .IsRequired();

            address.Property(value => value.Street)
                .HasColumnName("street")
                .HasMaxLength(200)
                .IsRequired();

            address.Property(value => value.Building)
                .HasColumnName("building")
                .HasMaxLength(50);

            address.Property(value => value.OfficeNumber)
                .HasColumnName("office_number")
                .HasMaxLength(50);
        });

        builder.HasIndex(location => location.Name)
            .HasDatabaseName("ix_locations_name");

        builder.Ignore(location => location.Departments);
    }
}
