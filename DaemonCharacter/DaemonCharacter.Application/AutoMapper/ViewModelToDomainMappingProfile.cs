using AutoMapper;
using DaemonCharacter.Application.ViewModels.Attribute;
using DaemonCharacter.Application.ViewModels.Campaign;
using DaemonCharacter.Application.ViewModels.CharacterAttribute;
using DaemonCharacter.Application.ViewModels.Item;
using DaemonCharacter.Application.ViewModels.NonPlayer;
using DaemonCharacter.Application.ViewModels.Player;
using DaemonCharacter.Application.ViewModels.PlayerItem;
using DaemonCharacter.Domain.Entities;

namespace DaemonCharacter.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AttributeViewModel, Attributes>();
            CreateMap<AttributeBonusViewModel, Attributes>();

            CreateMap<PlayerViewModel, Player>();
            CreateMap<PlayerItemViewModel, PlayerItem>();
            
            CreateMap<CampaignViewModel, Campaign>();

            CreateMap<CharacterAttributeViewModel, CharacterAttribute>();
            CreateMap<SelectedCharacterAttributeViewModel, CharacterAttribute>();

            CreateMap<NonPlayerViewModel, NonPlayer>();

            CreateMap<ItemViewModel, Item>();
        }
    }
}
