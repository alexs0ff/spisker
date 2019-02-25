using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Server.Core.Common.Entities.Files;
using Server.Core.Common.Entities.Lists;
using Server.Core.Common.Entities.Social;
using Server.Core.Common.Entities.Users;

namespace Server.Core.Common.Contexts
{
    public class StartenumEntities: DbContext
    {
        /// <summary>
        ///     <para>
        ///         Initializes a new instance of the <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> class using the specified options.
        ///         The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method will still be called to allow further
        ///         configuration of the options.
        ///     </para>
        /// </summary>
        /// <param name="options">The options for this context.</param>
        internal StartenumEntities(DbContextOptions options) : base(options)
        {
            
        }

        /// <summary>
        ///     Override this method to further configure the model that was discovered by convention from the entity types
        ///     exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        ///     and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <remarks>
        ///     If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        ///     then this method will not be run.
        /// </remarks>
        /// <param name="modelBuilder">
        ///     The builder being used to construct the model for this context. Databases (and other extensions) typically
        ///     define extension methods on this object that allow you to configure aspects of the model that are specific
        ///     to a given database.
        /// </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLogin>().HasKey(c => new {c.LoginProvider, c.ProviderKey});
        }

        public virtual DbSet<List> List { get; set; }
        public virtual DbSet<ListItem> ListItem { get; set; }
        public virtual DbSet<ListItemUserLikeMap> ListItemUserLikeMap { get; set; }
        public virtual DbSet<ListUserLikeMap> ListUserLikeMap { get; set; }
        public virtual DbSet<PortalUser> PortalUser { get; set; }
        public virtual DbSet<UserClaim> UserClaim { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<PortalUserProfile> PortalUserProfile { get; set; }
        public virtual DbSet<UserFollowingMap> UserFollowingMap { get; set; }
        public virtual DbSet<ListCheckItemKind> ListCheckItemKind { get; set; }
        public virtual DbSet<ListKind> ListKind { get; set; }
        public virtual DbSet<StoredFile> StoredFile { get; set; }
        public virtual DbSet<RecoveryLogin> RecoveryLogin { get; set; }
    }
}
