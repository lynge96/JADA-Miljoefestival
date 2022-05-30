using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiljøFestival.Shared.Models
{

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
            if (Rolle == "Koordinator")
            {
                return true;
            } else
            {
                return false;
            }
        }


        // Returnerer true / false om brugeren er formand (abstraktion?)
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
