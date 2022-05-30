using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiljøFestival.Shared.Models
{
    public class Vagt
    {
        public int Vagt_Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime Slut { get; set; }

        public string Taget_Af { get; set; }

        public int? Bruger_Id { get; set; }

        public string Opgave { get; set; }

        public int Opgave_Id { get; set; }

        public string Beskrivelse { get; set; }

        public string Status { get; set; }
    }
}
