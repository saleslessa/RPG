using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Repository;
using DaemonCharacter.Infra.Data.Context;

namespace DaemonCharacter.Infra.Data.Repository
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(DaemonCharacterContext context) : base(context)
        {
        }
    }
}
