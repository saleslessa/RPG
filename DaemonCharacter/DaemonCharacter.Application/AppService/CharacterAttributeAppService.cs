﻿using DaemonCharacter.Application.Interfaces;
using System;
using System.Collections.Generic;
using DaemonCharacter.Infra.Data.Interfaces;
using DaemonCharacter.Application.ViewModels.CharacterAttribute;
using AutoMapper;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Service;

namespace DaemonCharacter.Application.AppService
{
    public class CharacterAttributeAppService : ApplicationService, ICharacterAttributeAppService
    {

        private readonly ICharacterAttributeService _characterAttributeService;
        private readonly IAttributeService _attributeService;

        public CharacterAttributeAppService(ICharacterAttributeService characterAttributeService, IAttributeService attributeService, IUnitOfWork uow) : base(uow)
        {
            _characterAttributeService = characterAttributeService;
            _attributeService = attributeService;
        }

        public CharacterAttributeViewModel Add(CharacterAttributeViewModel model)
        {
            var obj = Mapper.Map<CharacterAttributeViewModel, CharacterAttribute>(model);

            Commit();

            return Mapper.Map<CharacterAttribute, CharacterAttributeViewModel>(_characterAttributeService.Add(obj));
        }

        public void Dispose()
        {
            _characterAttributeService.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<CharacterAttributeViewModel> ListAllAttributes()
        {
            return Mapper.Map<IEnumerable<CharacterAttribute>, IEnumerable<CharacterAttributeViewModel>>(_characterAttributeService.ListAllAttributes());
        }

        public IEnumerable<CharacterAttributeViewModel> ListFromCharacter(Guid CharacterId)
        {
            return Mapper.Map<IEnumerable<CharacterAttribute>, IEnumerable<CharacterAttributeViewModel>>(_characterAttributeService.ListFromCharacter(CharacterId));
        }

        public void Remove(Guid CharacterAttributeId)
        {
            _characterAttributeService.Remove(CharacterAttributeId);
            Commit();
        }

        public void RemoveFromCharacter(Guid CharacterId)
        {
            _characterAttributeService.RemoveFromCharacter(CharacterId);
            Commit();
        }

    }
}