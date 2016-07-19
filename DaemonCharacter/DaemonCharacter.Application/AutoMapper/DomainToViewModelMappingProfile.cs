﻿using AutoMapper;
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
    public class DomainToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Attributes, AttributeViewModel>();
            CreateMap<Attributes, AttributeBonusViewModel>();

            CreateMap<Player, PlayerViewModel>();
            CreateMap<PlayerItem, PlayerItemViewModel>();

            CreateMap<Campaign, PlayerCampaignViewModel>();
            CreateMap<Campaign, CampaignViewModel>();

            CreateMap<CharacterAttribute, CharacterAttributeViewModel>();

            CreateMap<NonPlayer, NonPlayerViewModel>();

            CreateMap<Item, ItemViewModel>();
        }
    }
}
