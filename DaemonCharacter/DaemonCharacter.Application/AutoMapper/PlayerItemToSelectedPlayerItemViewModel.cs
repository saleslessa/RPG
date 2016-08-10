using DaemonCharacter.Application.ViewModels.PlayerItem;
using DaemonCharacter.Domain.Entities;
using System.Collections.Generic;

namespace DaemonCharacter.Application.AutoMapper
{
    public class PlayerItemToSelectedPlayerItemViewModel
    {
        public IEnumerable<SelectedPlayerItemViewModel> Map(IEnumerable<PlayerItem> model)
        {
            var result = new List<SelectedPlayerItemViewModel>();

            foreach (var item in model)
            {
                result.Add(new SelectedPlayerItemViewModel()
                {
                    CharacterId = item.Player.CharacterId,
                    ItemId = item.Item.ItemId,
                    ItemName = item.Item.ItemName,
                    PlayerItemApprovedByMaster = item.PlayerItemApprovedByMaster,
                    PlayerItemDateBought = item.PlayerItemDateBought,
                    PlayerItemQtd = item.PlayerItemQtd,
                    PlayerItemUnitPrice = item.PlayerItemUnitPrice
                });
            }

            return result;
        }

    }
}
