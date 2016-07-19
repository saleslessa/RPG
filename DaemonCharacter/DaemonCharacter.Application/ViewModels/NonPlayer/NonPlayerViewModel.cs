using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaemonCharacter.Application.ViewModels.NonPlayer
{
    public class NonPlayerViewModel
    {
        [Key]
        public Guid CharacterId { get; set; }

        [DisplayName("Name")]
        
        public string CharacterName { get; set; }

        [DisplayName("Life")]
        public int CharacterMaxLife { get; set; }

        [DisplayName("Type")]
        public NonPlayerTypes NonPlayerType { get; set; }

        [DisplayName("Chalenge Level")]
        public int NonPlayerChalengeLevel { get; set; }

        [DisplayName("Public Annotations")]
        public string NonPlayerPublicAnnotations { get; set; }

        [ScaffoldColumn(false)]
        public DomainValidation.Validation.ValidationResult ValidationResult { get; set; }

        public NonPlayerViewModel()
        {
            CharacterId = Guid.NewGuid();
            ValidationResult = new DomainValidation.Validation.ValidationResult();
        }
    }
}
