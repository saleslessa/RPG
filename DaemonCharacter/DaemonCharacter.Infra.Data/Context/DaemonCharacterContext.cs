using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Infra.Data.EntityConfig;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DaemonCharacter.Infra.Data.Context
{
    public class DaemonCharacterContext : DbContext
    {
        public DaemonCharacterContext()
            : base("DefaultConnection")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<DaemonCharacterContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(255));

            //modelBuilder.Properties<int>()
            //    .Configure(p => p.HasColumnType("smallint"));


            modelBuilder.Configurations.Add(new AttributeConfig());
            modelBuilder.Configurations.Add(new CampaignConfig());
            modelBuilder.Configurations.Add(new PlayerConfig());

            //modelBuilder.Configurations.Add(new ItemConfig());
            //modelBuilder.Configurations.Add(new ItemAttributeConfig());

            //modelBuilder.Configurations.Add(new SessionConfig());

            //modelBuilder.Configurations.Add(new CharacterAttributeConfig());
            //modelBuilder.Configurations.Add(new NonPlayerConfig());


            //modelBuilder.Configurations.Add(new PlayerItemConfig());
            //modelBuilder.Configurations.Add(new PlayerSessionConfig());

            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<Attributes> Attributes { get; set; }
        public IDbSet<Campaign> Campaigns { get; set; }
        public IDbSet<Player> Players { get; set; }

        //public DbSet<CharacterAttributes> CharacterAttributes { get; set; }



        //public DbSet<NonPlayers> NonPlayers { get; set; }

        //public DbSet<Sessions> CampaignSession { set; get; }

        //public DbSet<Items> Items { get; set; }

        //public DbSet<ItemAttributes> ItemAttributes { get; set; }

    }

    public static class ChangeDb
    {
        public static void ChangeConnection(this DaemonCharacterContext context, string cn)
        {
            context.Database.Connection.ConnectionString = cn;
        }
    }
}
