using System;

namespace DaemonCharacter.Domain.Entities
{
    public class PlayerItems
    {

        public Guid PlayerItemId { get; set; }

        public Player Player { get; set; }

        public Items Item { get; set; }

        public int PlayerItemQtd { get; set; }

        public DateTime PlayerItemDateBought { get; set; }

        public int PlayerItemUnitPrice { get; set; }

        public bool PlayerItemApprovedByMaster { get; set; }

        public PlayerItems()
        {
            PlayerItemId = Guid.NewGuid();
        }
    }
}