
using System.Collections.Generic;

namespace MiljøFestival.Shared.Models
{

    public class Bruger
    {
        public int Bruger_Id { get; set; }
        public string Fornavn { get; set; }

        public string Efternavn { get; set; }

        public string Telefon { get; set; }

        public string Email { get; set; }

        public string Adresse { get; set; }

        public int? Rolle_Id { get; set; }

        public string Rolle { get; set; }

        public int? Team_Id { get; set; }

        public string Team { get; set; }

        public string Kode { get; set; }

        public List<Kompetence> Kompetences { get; set; }
         

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



    // Metode til ErLoggetPå -> returnerer true / false
    // Metode til at ændre koordinatorstatus
}
