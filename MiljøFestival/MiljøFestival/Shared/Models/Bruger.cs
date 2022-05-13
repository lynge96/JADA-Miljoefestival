using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiljøFestival.Shared.Models
{
    public class Bruger
    {
        public int Bruger_Id { get; set; }

        [Required]
        public string Fornavn { get; set; }

        [Required]
        public string Efternavn { get; set; }

        [Required]
        public string Telefon { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Adresse { get; set; }

        public bool Koordinator { get; set; }

        [Required]
        public string Kode { get; set; }

    }
}
