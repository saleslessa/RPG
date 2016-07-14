using DomainValidation.Validation;
using System;

namespace DaemonCharacter.Domain.Entities
{

    public class Character
    {

        public Guid CharacterId { get; set; }

        public string CharacterName { get; set; }

        public int CharacterMaxLife { get; set; }

        public int CharacterRemainingLife { get; set; }

        public string CharacterUser { get; set; }

        public Races CharacterRace { get; set; }

        public Genders CharacterGender { get; set; }

        public virtual Campaign Campaign { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public Character()
        {
            CharacterId = Guid.NewGuid();
            ValidationResult = new ValidationResult();
        }
    }


    //[Table("tb_nonplayer_session")]
    //public class NonPlayerCampaignModel
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    [Column(Order = 0)]
    //    public int id { get; set; }

    //    [Key, Column(Order = 1)]
    //    public int idSession { set; get; }

    //    [Required, Display(Name = "Remaining Life Points"), Range(0, int.MaxValue), DefaultValue(0)]
    //    public int remainingLife { get; set; }

    //    [DataType(DataType.MultilineText)]
    //    [Display(Name = "Annotations that only you (master) can see")]
    //    public string privateAnnotations { get; set; }

    //    public virtual NonPlayerModel nonplayer { get; set; }

    //    [ForeignKey("idSession")]
    //    public virtual CampaignSessionModel session { get; set; }
    //}

}
