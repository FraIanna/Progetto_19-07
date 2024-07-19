using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data
{
    public class VerbalEntity
    {
        public int Id { get; set; }
        public int RegistryId { get; set; }
        public int ViolationId { get; set; }

        [Display(Name = "Data Violazione")]
        [Required]
        public DateTime ViolationData { get; set; }

        [Display(Name = "Indirizzo Violazione")]
        [Required]
        public string ViolationAddress { get; set; }

        [Display(Name = "Nome dell'agente")]
        [Required]
        public string OfficerName { get; set; }

        [Display(Name = "Data della trascrizione verbale")]
        [Required]
        public DateTime VerbalTranscription { get; set; }

        [Display(Name = "Decurtamento punti")]
        public int LicencePoints { get; set; }

        [Display(Name = "Multa contestata")]
        [Required]
        public bool ContestedTicket { get; set; }
    }
}
