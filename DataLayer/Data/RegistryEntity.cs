using System.ComponentModel.DataAnnotations;

namespace DataLayer.Data
{
    public class RegistryEntity
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Display(Name = "Cognome")]
        [Required]
        [StringLength(30)]
        public string Surname { get; set; }

        [Display(Name = "Indirizzo")]
        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Display(Name = "Città")]
        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Display(Name = "Cap")]
        [Required]
        [StringLength(5)]
        public string Cap { get; set; }

        [Display(Name = "Codice Fiscale")]
        [Required]
        [StringLength(16)]
        public string CodiceFiscale { get; set; }
    }
}
