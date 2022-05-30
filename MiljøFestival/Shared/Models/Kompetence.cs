
using System.ComponentModel.DataAnnotations;

namespace MiljøFestival.Shared.Models
{
    public class Kompetence
    {
        public int Bruger_Id { get; set; }  
        public int Kompetence_Id { get; set; }
        public string Kompetence_Navn { get; set; }
        public bool IsChecked { get; set; }
    }
}
