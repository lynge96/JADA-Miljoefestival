using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiljøFestival.Shared.Models
{
    // I Bruger-klassen bruger vi EditForm ved oprettelse af en bruger.
    // Derfor finder man [REQUIRED] m.m. over nogle properties, som bruges
    // ved til validering.

    public class Bruger
    {
        public int Bruger_Id { get; set; }

        [Required (ErrorMessage = "Indtast dit fornavn")]
        public string Fornavn { get; set; }

        [Required(ErrorMessage = "Indtast dit efternavn")]
        public string Efternavn { get; set; }

        [Required(ErrorMessage = "Indtast dit telefonnummer")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Indtast din emailadresse")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Indtast en gyldig emailadresse")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Indtast din adresse")]
        public string Adresse { get; set; }

        public int? Rolle_Id { get; set; }

        public string Rolle { get; set; }

        public int? Team_Id { get; set; }

        [Required(ErrorMessage = "Vælg et team fra listen")]
        public string Team { get; set; }

        [Required(ErrorMessage = "Indtast et gyldig password")]
        public string Kode { get; set; }

        public List<Kompetence> Kompetencer { get; set; }
         

        // Returnerer true / false hvis brugeren er logget på eller ej
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


        // Returnerer true / false hvis brugeren er koordinator eller ej
        public bool ErKoordinator()
        {
            if (Rolle == "Koordinator")
            {
                return true;
            } else
            {
                return false;
            }
        }


        // Returnerer true / false hvis brugeren er formand eller ej
        public bool ErFormand()
        {
            if (Rolle == "Formand")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
