using System;

namespace DaemonCharacter.Domain.Entities
{
    public class CharacterAttributes
    {
        public Guid CharacterAttributeId { get; set; }

        public virtual Character Character { get; set; }

        public virtual Attributes Attribute { get; set; }

        public int CharacterAttributeValue { get; set; }


        public CharacterAttributes()
        {
            CharacterAttributeId = Guid.NewGuid();
        }
        //public virtual List<CharacterAttributeModel> bonusValues { get; set; }
    }
}