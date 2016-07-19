using System;

namespace DaemonCharacter.Domain.Entities
{

    public class ItemAttributes
    {
        public Guid ItemAttributeId { get; set; }

        public virtual Item Item { get; set; }

        public virtual Attributes Attribute { get; set; }

        public int ItemAttributeValue { get; set; }

        public ItemAttributes() { }

        internal ItemAttributes(Attributes _att, int _value)
        {
            ItemAttributeId = Guid.NewGuid();
            this.Attribute = _att;
            this.ItemAttributeValue = _value;
        }
    }
}