using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DaemonCharacter.Models
{
    [Table("tb_player_session")]
    public class PlayerSessionModel
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Key, Column(Order = 1)]
        public int idSession { get; set; }

        [Key, Column(Order = 2)]
        public int idPlayer { get; set; }

        [ForeignKey("idSession")]
        public SessionModel session { get; set; }

        [ForeignKey("idPlayer")]
        public PlayerModel player { get; set; }

        [Display(Name = "Public Annotations"), DataType(DataType.MultilineText)]
        public string publicAnnotations { get; set; }

        [Display(Name = "Private Annotations"), DataType(DataType.MultilineText)]
        public string privateAnnotations { get; set; }

    }

}