using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Entities;

namespace UrlShortener.Data.Mappings
{
    public class UrlMapping : IEntityTypeConfiguration<Url>
    {
        public void Configure(EntityTypeBuilder<Url> builder)
        {
            builder.ToTable("urls");

            builder.HasKey(x => x.Id)
                .HasName("PK_id");
            
            builder.HasIndex(x => x.Short)
                .HasDatabaseName("IX_short")
                .IsUnique();
            
            builder.Property(x => x.Id)
                .HasColumnName("id");
            
            builder.Property(x => x.Short)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(7)
                .HasColumnName("short");
            
            builder.Property(x => x.LongUrl)
                .IsRequired()
                .HasColumnName("long_url");
            
            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");
        }
    }
}
