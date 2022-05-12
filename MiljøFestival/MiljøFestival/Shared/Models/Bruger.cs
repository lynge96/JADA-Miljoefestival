using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiljøFestival.Shared.Models
{
    public class Bruger
    {
        public int Bruger_Id { get; set; }
        
        public string Fornavn { get; set; }

        public string Efternavn { get; set; }

        public int Telefon { get; set; }

        public string Email { get; set; }

        public string Adresse { get; set; }

        public bool Koordinator { get; set; }


    }
}
