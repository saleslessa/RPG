using DaemonCharacter.Application.ViewModels.PlayerItem;
using DaemonCharacter.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<SelectedPlayerItemViewModel>> MapAsync(IEnumerable<PlayerItem> model)
        {
            return await Task.Run(() => Map(model));
        }

    }
}
