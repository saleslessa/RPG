﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DaemonCharacter.Models
{
    [Table("tb_campaign")]
    public class CampaignModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Campaign name is required. Please chose one")]
        [Display(Name = "Campaign Name"), DataType(DataType.Text), MaxLength(50)]
        public string name { get; set; }

        [Display(Name = "Short Description"), DataType(DataType.MultilineText), MaxLength(255)]
        public string shortDescription { get; set; }

        [Required(ErrorMessage = "Every campaign must have a briefing. Please use your imagination to create one :)")]
        [Display(Name = "Campaign Briefing"), DataType(DataType.MultilineText), MaxLength(5000)]
        public string briefing { get; set; }

        [Display(Name = "Start Year"), DefaultValue(0)]
        public int startYear { get; set; }

        [Required, Range(1, 7), DefaultValue(2)]
        [Display(Name = "Maximum participants")]
        public int maxPlayers { get; set; }

        [Required, DefaultValue(0)]
        [Display(Name = "Remaining participants")]
        public int remainingPlayers { get; set; }

        [Display(Name = "Campaign image"), DataType(DataType.ImageUrl)]
        public byte[] img { get; set; }

        [DefaultValue(CampaignStatus.Beginning), Display(Name = "Campaign Status")]
        public CampaignStatus campaignStatus { get; set; }

        public virtual UserProfileModel userMaster { get; set; }


    }


    [Table("tb_campaign_session")]
    public class CampaignSessionModel
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }


        [Key, Column(Order = 1)]
        [Display(Name = "Date Scheduled"), DataType(DataType.Date)]
        public DateTime dayScheduled { get; set; }

        [Key, Column(Order = 2)]
        [Display(Name = "Campaign")]
        public int idCampaign { get; set; }

        [ForeignKey("idCampaign")]
        public virtual CampaignModel campaign { get; set; }

        [Required]
        [Display(Name = "Time Scheduled"), DataType(DataType.Time)]
        public DateTime timeScheduled { get; set; }

        [Display(Name = "Session Briefing"), DefaultValue("")]
        public string briefing { get; set; }

        [Display(Name = "Annotations before session"), DataType(DataType.MultilineText)]
        public string beforeAnnotations { get; set; }

        [Display(Name = "Annotations during session"), DataType(DataType.MultilineText)]
        public string duringAnnotations { get; set; }

    }



    public class AvailableCampaignsModel
    {
        [Required]
        public int id { get; set; }

        [Display(Name = "Campaign Name"), DataType(DataType.Text), MaxLength(50)]
        public string name { get; set; }

        [Display(Name = "Short Description"), DataType(DataType.MultilineText), MaxLength(255)]
        public string shortDescription { get; set; }

        [Required]
        [Display(Name = "Remaining participants")]
        public int remainingPlayers { get; set; }

        public virtual UserProfileModel userMaster { get; set; }

    }
}