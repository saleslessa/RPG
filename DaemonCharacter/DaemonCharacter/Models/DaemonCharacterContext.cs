using System.Data.Entity;

namespace DaemonCharacter.Models
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
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<DaemonCharacter.Models.DaemonCharacterContext>());

        public DaemonCharacterContext() : base("name=DaemonCharacter")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<DaemonCharacterContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //New fluent API

            modelBuilder.Entity<CharacterModel>()
                .HasRequired(t => t.user)
                .WithMany()
                .Map(t => t.MapKey("idUser")); 

            modelBuilder.Entity<CharacterModel>()
                .HasRequired(t => t.race)
                .WithMany()
                .Map(t => t.MapKey("idRace"));


            modelBuilder.Entity<PlayerModel>()
                .HasOptional(t => t.campaign)
                .WithMany()
                .Map(t => t.MapKey("idCampaign"));

            modelBuilder.Entity<CharacterAttributeModel>()
                .HasOptional(t => t.character)
                .WithMany()
                .Map(t => t.MapKey("idCharacter"));

            modelBuilder.Entity<CharacterAttributeModel>()
                .HasOptional(t => t.attribute)
                .WithMany()
                .Map(t => t.MapKey("idAttribute"));

            modelBuilder.Entity<CampaignModel>()
                .HasRequired(t => t.userMaster)
                .WithMany()
                .Map(t => t.MapKey("idMaster"));

            modelBuilder.Entity<AttributeModel>()
                .HasOptional(t => t.type)
                .WithMany()
                .Map(t => t.MapKey("idType"));

            //modelBuilder.Entity<NonPlayerCampaignModel>()
            //    .HasOptional(t => t.nonplayer)
            //    .WithMany()
            //    .Map(t => t.MapKey("idNonPlayer"));

            //modelBuilder.Entity<NonPlayerCampaignModel>()
            //    .HasOptional(t => t.session)
            //    .WithMany()
            //    .Map(t => t.MapKey("idSession"));

            modelBuilder.Entity<AttributeModel>()
                .HasMany(t => t.ParentAttribute)
                .WithMany(t => t.AttributeBonus)
                .Map(t => t.ToTable("tb_attribute_bonus")
                    .MapLeftKey("idAttribute")
                    .MapRightKey("idAttributeBonus"));

            modelBuilder.Entity<ItemAttributeModel>()
                .HasOptional(t => t.item)
                .WithMany()
                .Map(t => t.MapKey("idItem"));

            modelBuilder.Entity<ItemAttributeModel>()
                .HasOptional(t => t.attribute)
                .WithMany()
                .Map(t => t.MapKey("idAttribute"));

        }

        public DbSet<AttributeTypeModel> AttributeTypes { get; set; }

        public DbSet<AttributeModel> Attributes { get; set; }

        public DbSet<CampaignModel> CampaignModels { get; set; }

        public DbSet<UserProfileModel> UserProfiles { get; set; }

        public DbSet<RaceModel> Races { get; set; }


        #region Characters 
        public DbSet<CharacterModel> Characters { get; set; }

        public DbSet<CharacterAttributeModel> CharacterAttributes { get; set; }

        //public DbSet<NonPlayerCampaignModel> NonPlayerCampaigns { get; set; }

        public DbSet<PlayerModel> Players { get; set; }


        public DbSet<NonPlayerModel> NonPlayers { get; set; }

        #endregion

        public DbSet<CampaignSessionModel> CampaignSession { set; get; }


    }
}
