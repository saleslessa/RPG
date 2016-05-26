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

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CampaignModel>().HasRequired(h => h.userMaster)
                .WithMany(u => u.campaigns)
                .HasForeignKey(c => c.idMaster)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CharacterModel>().HasRequired(h => h.user)
                .WithMany(u => u.characters)
                .HasForeignKey(c => c.idUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AttributeModel>().HasRequired(h => h.type)
                .WithMany(a => a.attributes)
                .HasForeignKey(a => a.idAttributeType)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<NonPlayerCampaignModel>().HasRequired(h => h.campaign)
                .WithMany(campaign => campaign.nonPlayerCampaigns)
                .HasForeignKey(f => f.idCampaign)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NonPlayerCampaignModel>().HasRequired(h => h.nonplayer)
                .WithMany(nonplayer => nonplayer.nonPlayerCampaigns)
                .HasForeignKey(f => f.idNonPlayer)
                .WillCascadeOnDelete(false);


        }

        public DbSet<AttributeTypeModel> AttributeTypes { get; set; }

        public DbSet<AttributeModel> Attributes { get; set; }

        public DbSet<AttributeBonusModel> AttributeBonus { get; set; }

        public DbSet<CampaignModel> CampaignModels { get; set; }

        public DbSet<UserProfileModel> UserProfiles { get; set; }

        public DbSet<RaceModel> Races { get; set; }


        #region Characters 
        public DbSet<CharacterModel> Characters { get; set; }

        public DbSet<CharacterAttributeModel> CharacterAttributes { get; set; }

        public DbSet<NonPlayerCampaignModel> NonPlayerCampaigns { get; set; }

        public DbSet<PlayerModel> Players { get; set; }

        public DbSet<NonPlayerTypeModel> NonPlayerTypes { get; set; }

        public DbSet<NonPlayerModel> NonPlayers { get; set; }

        public DbSet<GenderModel> Genders { get; set; }

        #endregion


    }
}
