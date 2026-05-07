using DictionaryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DictionaryService.Infrastructure.Postgres.Configurations;

internal sealed class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("posts");

        builder.HasKey(post => post.Id)
            .HasName("pk_posts");

        builder.Property(post => post.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(post => post.Title)
            .HasColumnName("title")
            .HasMaxLength(255)
            .HasConversion(ValueObjectConverters.NonEmptyString)
            .IsRequired();

        builder.Property(post => post.Description)
            .HasColumnName("description")
            .HasMaxLength(255)
            .HasConversion(ValueObjectConverters.NullableNonEmptyString);

        builder.HasIndex(post => post.Title)
            .HasDatabaseName("ix_posts_title");

        builder.HasMany(post => post.Departments)
            .WithOne(departmentPosition => departmentPosition.Post)
            .HasForeignKey(departmentPosition => departmentPosition.PostId)
            .HasConstraintName("fk_department_positions_post_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(post => post.Departments)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
