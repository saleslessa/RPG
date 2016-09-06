using DaemonCharacter.Application.AppService;
using DaemonCharacter.Application.Interfaces;
using DaemonCharacter.Domain.Interfaces.Repository;
using DaemonCharacter.Domain.Interfaces.Service;
using DaemonCharacter.Domain.Services;
using DaemonCharacter.Infra.Data.Context;
using DaemonCharacter.Infra.Data.Interfaces;
using DaemonCharacter.Infra.Data.Repository;
using DaemonCharacter.Infra.Data.UoW;
using SimpleInjector;

namespace DaemonCharacter.Infra.CrossCutting.IoC
{
    public class Bootstrapper
    {
        public static void RegisterServices(Container container)
        {
            // App
            container.Register<IAttributeAppService, AttributeAppService>(Lifestyle.Scoped);
            container.Register<ICampaignAppService, CampaignAppService>(Lifestyle.Scoped);
            container.Register<IPlayerAppService, PlayerAppService>(Lifestyle.Scoped);
            container.Register<ICharacterAttributeAppService, CharacterAttributeAppService>(Lifestyle.Scoped);
            container.Register<INonPlayerAppService, NonPlayerAppService>(Lifestyle.Scoped);
            container.Register<IItemAppService, ItemAppService>(Lifestyle.Scoped);
            container.Register<IPlayerItemAppService, PlayerItemAppService>(Lifestyle.Scoped);


            // Domain
            container.Register<IAttributeService, AttributeService>(Lifestyle.Scoped);
            container.Register<ICampaignService, CampaignService>(Lifestyle.Scoped);
            container.Register<IPlayerService, PlayerService>(Lifestyle.Scoped);
            container.Register<ICharacterAttributeService, CharacterAttributeService>(Lifestyle.Scoped);
            container.Register<INonPlayerService, NonPlayerService>(Lifestyle.Scoped);
            container.Register<IItemService, ItemService>(Lifestyle.Scoped);
            container.Register<IPlayerItemService, PlayerItemService>(Lifestyle.Scoped);
            container.Register<IItemAttributeService, ItemAttributeService>(Lifestyle.Scoped);         


            // Infra Data
            container.Register<IAttributeRepository, AttributeRepository>(Lifestyle.Scoped);
            container.Register<ICampaignRepository, CampaignRepository>(Lifestyle.Scoped);
            container.Register<ICharacterRepository, CharacterRepository>(Lifestyle.Scoped);
            container.Register<IPlayerRepository, PlayerRepository>(Lifestyle.Scoped);
            container.Register<ICharacterAttributeRepository, CharacterAttributeRepository>(Lifestyle.Scoped);
            container.Register<INonPlayerRepository, NonPlayerRepository>(Lifestyle.Scoped);
            container.Register<IItemRepository, ItemRepository>(Lifestyle.Scoped);
            container.Register<IPlayerItemRespository, PlayerItemRepository>(Lifestyle.Scoped);
            container.Register<IItemAttributeRepository, ItemAttributeRepository>(Lifestyle.Scoped);

            container.Register<DaemonCharacterContext>(Lifestyle.Scoped);
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
        }
    }
}
