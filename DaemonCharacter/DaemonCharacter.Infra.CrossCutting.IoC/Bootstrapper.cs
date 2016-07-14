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
            container.RegisterPerWebRequest<IAttributeAppService, AttributeAppService>();
            container.RegisterPerWebRequest<ICampaignAppService, CampaignAppService>();
            container.RegisterPerWebRequest<IPlayerAppService, PlayerAppService>();


            // Domain
            container.RegisterPerWebRequest<IAttributeService, AttributeService>();
            container.RegisterPerWebRequest<ICampaignService, CampaignService>();
            container.RegisterPerWebRequest<IPlayerService, PlayerService>();

            // Infra Dados
            container.RegisterPerWebRequest<IAttributeRepository, AttributeRepository>();
            container.RegisterPerWebRequest<ICampaignRepository, CampaignRepository>();
            container.RegisterPerWebRequest<IPlayerRepository, PlayerRepository>();


            container.RegisterPerWebRequest<IUnitOfWork, UnitOfWork>();
            container.RegisterPerWebRequest<DaemonCharacterContext>();
        }
    }
}
