using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Minibox.Shared.Library.Const;
using Minibox.Shared.Library.Extension;

namespace Minibox.Core.Data.Database.Main.Entity.Lang
{
	public class LanguageTranslation(Guid languageKeyId) : BaseEntity
	{
		public Guid Id { get; set; } = MiniboxExtensions.SequentialGuidGenerator.Generate();
		public Guid LanguageKeyId { get; set; } = languageKeyId;
		public string Code { get; set; } = string.Empty;
		public string Value { get; set; } = string.Empty;

		public virtual LanguageKey? LanguageKey { get; set; }
	}

	public class LanguageTranslationConfiguration : IEntityTypeConfiguration<LanguageTranslation>
	{
		public void Configure(EntityTypeBuilder<LanguageTranslation> builder)
		{
			builder.ToTable(nameof(LanguageTranslation), MiniboxConstants.DbSchema.Language);

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Code).HasMaxLength(4).IsRequired();
			builder.Property(x => x.Value).HasMaxLength(1000).IsRequired();

			builder.HasOne(x => x.LanguageKey).WithMany(x => x.LanguageTranslations).HasForeignKey(x => x.LanguageKeyId);
		}
	}
}
