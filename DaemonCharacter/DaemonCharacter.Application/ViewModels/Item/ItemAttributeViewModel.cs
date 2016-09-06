using DaemonCharacter.Domain.Entities;
using System.ComponentModel;

namespace DaemonCharacter.Application.ViewModels.Item
{
    public class ItemAttributeViewModel
    {    
        [DisplayName("Attribute")]
        public virtual Attributes Attribute { get; set; }

        [DisplayName("Bonus")]
        public int ItemAttributeValue { get; set; }
    }
}
