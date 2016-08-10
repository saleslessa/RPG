using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DaemonCharacter.Application.ViewModels.Item
{
    public class ItemViewModel
    {
        [Key]
        public Guid ItemId { get; set; }

        [DisplayName("Name")]
        public string ItemName { get; set; }

        [DisplayName("Effect")]
        public string ItemEffect { get; set; }

        [DisplayName("Price")]
        //[DataType(DataType.Currency)]
        public double ItemPrice { get; set; }

        [DisplayName("Unique Use")]
        public bool UniqueUse { get; set; }

        [DisplayName("Category")]
        public ItemCategory ItemCategory { get; set; }

        [ScaffoldColumn(false)]
        public DomainValidation.Validation.ValidationResult ValidationResult { get; set; }

        public ItemViewModel()
        {
            ItemId = Guid.NewGuid();
            ValidationResult = new DomainValidation.Validation.ValidationResult();
            UniqueUse = false;
        }
    }
}
