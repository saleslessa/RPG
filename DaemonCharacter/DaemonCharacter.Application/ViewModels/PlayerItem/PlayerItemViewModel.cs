﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using DaemonCharacter.Application.ViewModels.Item;

namespace DaemonCharacter.Application.ViewModels.PlayerItem
{
    public class PlayerItemViewModel
    {
        [ScaffoldColumn(false)]
        public Guid PlayerId { get; set; }

        [DisplayName("Available Items")]
        public List<ItemViewModel> ListAvailableItems { get; set; }
        
        [DisplayName("Selected Items")]
        public List<SelectedPlayerItemViewModel> SelectedItems { get; set; }

        [ScaffoldColumn(false)]
        public DomainValidation.Validation.ValidationResult ValidationResult { get; set; }

        public PlayerItemViewModel()
        {
            ValidationResult = new DomainValidation.Validation.ValidationResult();
            ListAvailableItems = new List<ItemViewModel>();
            SelectedItems = new List<SelectedPlayerItemViewModel>();
        }

    }
}