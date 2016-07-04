using AutoMapper;
using DaemonCharacter.Application.ViewModels.Attribute;
using DaemonCharacter.Application.ViewModels.Player;
using DaemonCharacter.Domain.Entities;

namespace DaemonCharacter.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Attributes, AttributeViewModel>();
            CreateMap<Attributes, AttributeBonusViewModel>();

            CreateMap<Player, PlayerViewModel>();
            CreateMap<PlayerCampaignViewModel, Campaign>();
        }
    }
}
