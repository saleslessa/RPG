using System.Data.Entity;

namespace DaemonCharacter.Domain
{
    public class DaemonCharacterContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<DaemonCharacter.Domain.DaemonCharacterContext>());

        public DaemonCharacterContext() : base("name=DaemonCharacter")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<DaemonCharacterContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //New fluent API

            modelBuilder.Entity<Character>()
                .HasRequired(t => t.user)
                .WithMany()
                .Map(t => t.MapKey("idUser")); 

            modelBuilder.Entity<Player>()
                .HasOptional(t => t.campaign)
                .WithMany()
                .Map(t => t.MapKey("idCampaign"));

            modelBuilder.Entity<CharacterAttribute>()
                .HasOptional(t => t.character)
                .WithMany()
                .Map(t => t.MapKey("idCharacter"));

            modelBuilder.Entity<CharacterAttribute>()
                .HasOptional(t => t.attribute)
                .WithMany()
                .Map(t => t.MapKey("idAttribute"));

            modelBuilder.Entity<Campaign>()
                .HasRequired(t => t.userMaster)
                .WithMany()
                .Map(t => t.MapKey("idMaster"));

            //modelBuilder.Entity<NonPlayerCampaignModel>()
            //    .HasOptional(t => t.nonplayer)
            //    .WithMany()
            //    .Map(t => t.MapKey("idNonPlayer"));

            //modelBuilder.Entity<NonPlayerCampaignModel>()
            //    .HasOptional(t => t.session)
            //    .WithMany()
            //    .Map(t => t.MapKey("idSession"));

            modelBuilder.Entity<Attributes>()
                .HasMany(t => t.ParentAttribute)
                .WithMany(t => t.AttributeBonus)
                .Map(t => t.ToTable("tb_attribute_bonus")
                    .MapLeftKey("idAttribute")
                    .MapRightKey("idAttributeBonus"));

            modelBuilder.Entity<ItemAttribute>()
                .HasRequired(t => t.item)
                .WithMany()
                .Map(t => t.MapKey("idItem"));

            modelBuilder.Entity<ItemAttribute>()
                .HasRequired(t => t.attribute)
                .WithMany()
                .Map(t => t.MapKey("idAttribute"));

        }

    }
}
