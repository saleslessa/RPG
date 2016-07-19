using DomainValidation.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [DataType(DataType.Currency)]
        public int ItemPrice { get; set; }

        [ScaffoldColumn(false)]
        public DomainValidation.Validation.ValidationResult ValidationResult { get; set; }

        public ItemViewModel()
        {
            ItemId = Guid.NewGuid();
            ValidationResult = new DomainValidation.Validation.ValidationResult();
        }
    }
}
