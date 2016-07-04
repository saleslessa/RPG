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

            // Domain
            container.RegisterPerWebRequest<IAttributeService, AttributeService>();

            // Infra Dados
            container.RegisterPerWebRequest<IAttributeRepository, AttributeRepository>();
            container.RegisterPerWebRequest<IUnitOfWork, UnitOfWork>();
            container.RegisterPerWebRequest<DaemonCharacterContext>();
        }
    }
}
