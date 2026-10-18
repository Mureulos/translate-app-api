using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using translate_app.Domain.Entities;

namespace translate_app.Infrastructure.Data.Configurations;

public sealed class TranslationRecordConfiguration : IEntityTypeConfiguration<TranslationRecord>
{
    public void Configure(EntityTypeBuilder<TranslationRecord> builder)
    {
        builder.ToTable("TranslationRecords");

        builder.HasKey(record => record.Id);

        builder.Property(record => record.NormalizedSource)
            .HasMaxLength(600)
            .IsRequired();

        builder.Property(record => record.TargetLanguage)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(record => record.ContextTag)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(record => record.TranslatedText)
            .IsRequired();

        builder.Property(record => record.IsHumanVerified)
            .HasDefaultValue(false);

        builder.Property(record => record.CreatedAt)
            .HasDefaultValueSql("NOW()");

        builder.Property(record => record.UpdatedAt)
            .HasDefaultValueSql("NOW()");

        builder.HasIndex(record => record.IsHumanVerified);
        builder.HasIndex(record => new
        {
            record.NormalizedSource,
            record.TargetLanguage,
            record.ContextTag
        }).IsUnique();
        builder.HasIndex(record => new
        {
            record.TargetLanguage,
            record.ContextTag
        });
    }
}
