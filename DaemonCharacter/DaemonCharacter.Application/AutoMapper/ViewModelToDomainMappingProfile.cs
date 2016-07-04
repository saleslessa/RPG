using AutoMapper;
using DaemonCharacter.Application.ViewModels.Attribute;
using DaemonCharacter.Application.ViewModels.Player;
using DaemonCharacter.Domain.Entities;

namespace DaemonCharacter.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AttributeViewModel, Attributes>();

            CreateMap<PlayerViewModel, Player>();
            CreateMap<Campaign, PlayerCampaignViewModel>();
        }
    }
}
