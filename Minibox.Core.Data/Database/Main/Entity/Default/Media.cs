using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Minibox.Shared.Library.Extension;
using Minibox.Shared.Library.Const;
using Minibox.Core.Data.Database.Main.Entity.Auth;

namespace Minibox.Core.Data.Database.Main.Entity.Default
{
	public class Media(int type, string url) : BaseEntity
	{
		public Guid Id { get; set; } = MiniboxExtensions.SequentialGuidGenerator.Generate();

		public int Type { get; set; } = type;

		public string Url { get; set; } = url;

		public virtual ICollection<User> Users { get; set; } = [];
	}

	public class MediaConfiguration : IEntityTypeConfiguration<Media>
	{
		public void Configure(EntityTypeBuilder<Media> builder)
		{
			builder.ToTable(nameof(Media), MiniboxConstants.DbSchema.Default);

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Type).IsRequired();
			builder.Property(x => x.Url).HasMaxLength(1000).IsRequired();
		}
	}
}
