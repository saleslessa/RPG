using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DaemonCharacter.Application.ViewModels.Attribute
{
    public class AttributeViewModel
    {
        public AttributeViewModel()
        {
            AttributeId = Guid.NewGuid();
            ParentAttribute = new List<AttributeBonusViewModel>();
            AttributeBonus = new List<AttributeBonusViewModel>();
        }

        [Key]
        public Guid AttributeId { get; set; }

        [Required(ErrorMessage ="Attribute Name is required.")]
        [DisplayName("Name")]
        [MaxLength(50, ErrorMessage = "Maximum allowed is {0} characters")]
        public string AttributeName { get; set; }

        [DisplayName("Description")]
        [MaxLength(50, ErrorMessage = "Maximum allowed is {0} characters")]
        public string AttributeDescription { get; set; }

        [Required(ErrorMessage ="Type is required. Please select one")]
        [DisplayName("Type")]
        public AttributeType AttributeType { get; set; }

        [DisplayName("Minimum")]
        [Range(0, short.MaxValue, ErrorMessage ="Maximum value allowed for this field is {1}")]
        public int AttributeMinimum { get; set; }
        
        [ScaffoldColumn(false)]
        public virtual ICollection<AttributeBonusViewModel> ParentAttribute { get; set; }

        [DisplayName("Bonus to")]
        public virtual ICollection<AttributeBonusViewModel> AttributeBonus { get; set; }

        [ScaffoldColumn(false)]
        public DomainValidation.Validation.ValidationResult ValidationResult { get; set; }

    }
}
