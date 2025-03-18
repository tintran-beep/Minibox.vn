using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Minibox.Shared.Library.Const;
using Minibox.Shared.Library.Extension;

namespace Minibox.Core.Data.Database.Main.Entity.Default
{
	public class Log
	{
		public Guid Id { get; set; } = MiniboxExtensions.SequentialGuidGenerator.Generate();
		public DateTime Timestamp { get; set; } = DateTime.UtcNow;
		public string Level { get; set; } = string.Empty;
		public string Message { get; set; } = string.Empty;
		public string? Exception { get; set; }
		public string? StackTrace { get; set; }
		public string? Properties { get; set; } 
		public string? RequestPath { get; set; }
		public int? StatusCode { get; set; }
		public Guid? UserId { get; set; } 
		public string? RequestId { get; set; } 
		public string? SourceContext { get; set; } 
		public string? MachineName { get; set; } 
		public string? IPAddress { get; set; } 
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}

	public class LogConfiguration : IEntityTypeConfiguration<Log>
	{
		public void Configure(EntityTypeBuilder<Log> builder)
		{
			builder.ToTable(nameof(Log), MiniboxConstants.DbSchema.Default);

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Timestamp).IsRequired();
			builder.Property(x => x.Level).HasMaxLength(50).IsRequired();
			builder.Property(x => x.Message).IsRequired();
			builder.Property(x => x.Exception);
			builder.Property(x => x.StackTrace);
			builder.Property(x => x.Properties);
			builder.Property(x => x.RequestPath).HasMaxLength(500);
			builder.Property(x => x.StatusCode);
			builder.Property(x => x.UserId);
			builder.Property(x => x.RequestId).HasMaxLength(100);
			builder.Property(x => x.SourceContext).HasMaxLength(255);
			builder.Property(x => x.MachineName).HasMaxLength(255);
			builder.Property(x => x.IPAddress).HasMaxLength(50);
			builder.Property(x => x.CreatedAt).IsRequired();
		}
	}
}
