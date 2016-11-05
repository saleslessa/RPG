﻿using System;
using AutoMapper;
using DaemonCharacter.Application.ViewModels.Attribute;
using DaemonCharacter.Application.ViewModels.Campaign;
using DaemonCharacter.Application.ViewModels.Item;
using DaemonCharacter.Application.ViewModels.NonPlayer;
using DaemonCharacter.Application.ViewModels.Player;
using DaemonCharacter.Domain.Entities;

namespace DaemonCharacter.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        [Obsolete("Create a constructor and configure inside of your profile\'s constructor instead. Will be removed in 6.0")]
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
            CreateMap<ItemAttributeViewModel, ItemAttribute>();
        }
    }
}
