using System;
using System.Threading;
using System.Threading.Tasks;

using Brighid.Commands.Service;

using Microsoft.EntityFrameworkCore;

namespace Brighid.Commands.Database
{
    /// <summary>
    /// Context to use for database interactions.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext" /> class.
        /// </summary>
        /// <param name="options">Options to use when interacting with the database.</param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext" /> class.
        /// </summary>
        internal DatabaseContext()
        {
        }

        /// <summary>
        /// Gets or sets the buckets in the database.
        /// </summary>
        public virtual DbSet<Command> Commands { get; set; } = null!;

        /// <summary>
        /// Executes raw SQL.
        /// </summary>
        /// <param name="formattableString">The formattable sql to execute.</param>
        /// <param name="cancellationToken">Token used to cancel the operation.</param>
        /// <returns>The resulting task.</returns>
        public virtual Task ExecuteSqlInterpolated(FormattableString formattableString, CancellationToken cancellationToken = default)
        {
            return Database.ExecuteSqlInterpolatedAsync(formattableString, cancellationToken);
        }

        /// <summary>
        /// Reloads an entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to reload.</typeparam>
        /// <param name="entity">The entity to reload.</param>
        /// <param name="cancellationToken">Token to cancel the operation with.</param>
        /// <returns>The resulting task.</returns>
        public virtual Task ReloadEntity<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        {
            return Entry(entity!).ReloadAsync(cancellationToken);
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new Command.EntityConfig());
        }
    }
}
