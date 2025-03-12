using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Minibox.Shared.Library.Const;
using Minibox.Shared.Library.Extension;

namespace Minibox.Core.Data.Database.Main.Entity.Lang
{
	public class LanguageKey : BaseEntity
	{
		public LanguageKey()
		{

		}
		public Guid Id { get; set; } = MiniboxExtensions.SequentialGuidGenerator.Generate();
		public string Key { get; set; } = string.Empty;
		public string DefaultValue { get; set; } = string.Empty;

		public ICollection<LanguageTranslation> LanguageTranslations { get; set; } = [];
	}

	public class LanguageKeyConfiguration : IEntityTypeConfiguration<LanguageKey>
	{
		public void Configure(EntityTypeBuilder<LanguageKey> builder)
		{
			builder.ToTable(nameof(LanguageKey), MiniboxConstants.DbSchema.Language);

			builder.HasKey(x => x.Id);
			builder.HasIndex(x => x.Key);
			builder.Property(x => x.Key).HasMaxLength(250).IsRequired();
			builder.Property(x => x.DefaultValue).HasMaxLength(1000).IsRequired();
		}
	}
}
