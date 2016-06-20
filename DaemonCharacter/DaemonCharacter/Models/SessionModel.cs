using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DaemonCharacter.Models
{
    [Table("tb_campaign_session")]
    public class SessionModel
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }


        [Key, Column(Order = 1)]
        [Display(Name = "Date Scheduled"), DataType(DataType.Date)]
        public DateTime dayScheduled { get; set; }

        [Key, Column(Order = 2)]
        [Display(Name = "Campaign")]
        public int idCampaign { get; set; }

        [ForeignKey("idCampaign"), Display(Name ="Campaign")]
        public virtual CampaignModel campaign { get; set; }

        [Required]
        [Display(Name = "Time Scheduled"), DataType(DataType.Time)]
        public DateTime timeScheduled { get; set; }

        [Display(Name = "Session Briefing"), DefaultValue("")]
        public string briefing { get; set; }

        [Display(Name = "Private annotations before session"), DataType(DataType.MultilineText)]
        public string privateBeforeAnnotations { get; set; }

        [Display(Name = "Annotations during session"), DataType(DataType.MultilineText)]
        public string duringAnnotations { get; set; }

        [DefaultValue(SessionStatus.Planning)]
        public SessionStatus status { get; set; }

    }
}