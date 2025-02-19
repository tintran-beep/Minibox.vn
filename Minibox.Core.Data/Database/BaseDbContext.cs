using Microsoft.EntityFrameworkCore;

namespace Minibox.Core.Data.Database
{
	public class BaseDbContext(DbContextOptions options) : DbContext(options)
	{
	}
}
