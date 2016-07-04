using System.ComponentModel.DataAnnotations;

namespace DaemonCharacter.Domain
{
    public class ListCampaignsModel
    {
        [Required]
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public int qtdSessions { get; set; }

    }
}