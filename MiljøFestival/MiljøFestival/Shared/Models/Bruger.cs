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


        // Returnerer true / false om brugeren er logget på (abstraktion?)
        public bool ErLoggetPå()
        {
            if (Bruger_Id > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // Returnerer true / false om brugeren er koordinator (abstraktion?)
        public bool ErKoordinator()
        {
            if (Koordinator == true)
            {
                return true;
            } else
            {
                return false;
            }
        }

    }



    // Metode til ErLoggetPå -> returnerer true / false
    // Metode til at ændre koordinatorstatus
}
