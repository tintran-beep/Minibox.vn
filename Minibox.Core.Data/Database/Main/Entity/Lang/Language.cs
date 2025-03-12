using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minibox.Shared.Library.Const;
using Minibox.Shared.Library.Extension;

namespace Minibox.Core.Data.Database.Main.Entity.Lang
{
	public class Language : BaseEntity
	{
		public Guid Id { get; set; } = MiniboxExtensions.SequentialGuidGenerator.Generate();
		public string Code { get; set; } = string.Empty;
		public string Value { get; set; } = string.Empty;
	}

	public class LanguageConfiguration : IEntityTypeConfiguration<Language>
	{
		public void Configure(EntityTypeBuilder<Language> builder)
		{
			builder.ToTable(nameof(Language), MiniboxConstants.DbSchema.Language);

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Code).HasMaxLength(4).IsRequired();
			builder.Property(x => x.Value).HasMaxLength(100).IsRequired();
		}
	}
}
