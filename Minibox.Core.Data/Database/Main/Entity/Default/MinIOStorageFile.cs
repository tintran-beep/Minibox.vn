using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Minibox.Shared.Library.Const;
using Minibox.Shared.Library.Extension;
using Minibox.Core.Data.Database.Main.Entity.Auth;

namespace Minibox.Core.Data.Database.Main.Entity.Default
{
	public class MinIOStorageFile : BaseEntity
	{
		public Guid Id { get; set; } = MiniboxExtensions.SequentialGuidGenerator.Generate();
		public string FileName { get; set; } = string.Empty;
		public string FilePath { get; set; } = string.Empty;
		public string ContentType { get; set; } = string.Empty;
		public long FileSize { get; set; }
		public string? StorageBucket { get; set; }
		public bool IsPublic { get; set; } = false;
		public Guid? UploadedBy { get; set; }
		public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

		public virtual ICollection<User> Users { get; set; } = [];
	}

	public class MinIOStorageFileConfiguration : IEntityTypeConfiguration<MinIOStorageFile>
	{
		public void Configure(EntityTypeBuilder<MinIOStorageFile> builder)
		{
			builder.ToTable(nameof(MinIOStorageFile), MiniboxConstants.DbSchema.Default);

			builder.HasKey(x => x.Id);
			builder.Property(x => x.FileName).HasMaxLength(255).IsRequired();
			builder.Property(x => x.FilePath).HasMaxLength(500).IsRequired();
			builder.Property(x => x.ContentType).HasMaxLength(100).IsRequired();
			builder.Property(x => x.FileSize).IsRequired();
			builder.Property(x => x.StorageBucket).HasMaxLength(100);
			builder.Property(x => x.IsPublic).IsRequired();
			builder.Property(x => x.UploadedBy);
			builder.Property(x => x.UploadedAt).IsRequired();
		}
	}
}
