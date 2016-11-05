using System;
using AutoMapper;
using DaemonCharacter.Application.ViewModels.Attribute;
using DaemonCharacter.Application.ViewModels.Campaign;
using DaemonCharacter.Application.ViewModels.Item;
using DaemonCharacter.Application.ViewModels.NonPlayer;
using DaemonCharacter.Application.ViewModels.Player;
using DaemonCharacter.Domain.Entities;

namespace DaemonCharacter.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        [Obsolete("Create a constructor and configure inside of your profile\'s constructor instead. Will be removed in 6.0")]
        protected override void Configure()
        {
            CreateMap<Attributes, AttributeViewModel>();
            CreateMap<Attributes, AttributeBonusViewModel>();

            CreateMap<Player, PlayerViewModel>();
            CreateMap<PlayerItem, PlayerItemViewModel>();
            CreateMap<PlayerItem, SelectedPlayerItemViewModel>();

            CreateMap<Campaign, PlayerCampaignViewModel>();
            CreateMap<Campaign, CampaignViewModel>();

            CreateMap<CharacterAttribute, CharacterAttributeViewModel>();
            CreateMap<CharacterAttribute, SelectedCharacterAttributeViewModel>();


            CreateMap<NonPlayer, NonPlayerViewModel>();


            CreateMap<Item, ItemViewModel>();
            CreateMap<ItemAttribute, ItemAttributeViewModel>();
        }
    }
}
