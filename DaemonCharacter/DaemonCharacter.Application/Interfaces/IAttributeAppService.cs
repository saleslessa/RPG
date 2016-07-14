﻿using DaemonCharacter.Application.ViewModels.Attribute;
using DaemonCharacter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Application.Interfaces
{
    public interface IAttributeAppService : IDisposable
    {
        AttributeViewModel Add(AttributeViewModel _AttributeViewModel);

        AttributeViewModel Get(Guid? AttributeId);

        IEnumerable<AttributeViewModel> ListAll();

        AttributeViewModel Update(AttributeViewModel _AttributeViewModel);

        void Remove(Guid AttributeId);

        IEnumerable<AttributeBonusViewModel> ListAvailableForBonus(Guid? SelectedAttribute);

    }
}