using DaemonCharacter.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class UserConfig : EntityTypeConfiguration<Users>
    {
        public UserConfig()
        {
            
        }
    }
}
