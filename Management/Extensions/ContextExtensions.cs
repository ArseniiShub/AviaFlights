using Microsoft.EntityFrameworkCore;

namespace Management.Extensions;

public static class ContextExtensions
{
	/// <summary>
	/// Applies <see cref="DeleteBehavior.Restrict"/> <see cref="DeleteBehavior"/>
	/// to all foreign keys with <see cref="DeleteBehavior.Cascade"/> <see cref="DeleteBehavior"/>
	/// </summary>
	public static void SetOnDeleteRestrict(this ModelBuilder modelBuilder)
	{
		foreach(var entityType in modelBuilder.Model.GetEntityTypes())
		{
			var foreignKeys = entityType.GetForeignKeys()
				.Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

			foreach(var fk in foreignKeys)
			{
				fk.DeleteBehavior = DeleteBehavior.Restrict;
			}
		}
	}
}
