using System;

namespace DaemonCharacter.Domain.Entities
{

    public class Items
    {
        public Guid ItemId { get; set; }

        public string ItemName { get; set; }

        public string ItemEffect { get; set; }

        public int ItemPrice { get; set; }


        public Items()
        {
            ItemId = new Guid();
        }
    }

}