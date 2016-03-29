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

        public DaemonCharacterContext() : base("name=DaemonCharacterContext")
        {
            System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<DaemonCharacter.Models.DaemonCharacterContext>());
        }

        public DbSet<AttributeType> AttributeTypes { get; set; }

        public DbSet<AttributeClass> AttributeClasses { get; set; }
    }
}
