using MusicLib.Framework.EntityConfigurations;
using System.Data.Entity;
using System.Reflection;

namespace MusicLib.Framework
{
    public class MusicLibDbContext : DbContext
    {

        public MusicLibDbContext() : base ("FileDatabaseConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MusicLibDbContext>());
            Configuration.LazyLoadingEnabled = true;
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(AudioFileConfiguration)));
        }
    }
}
