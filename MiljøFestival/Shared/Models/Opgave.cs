namespace MiljøFestival.Shared.Models
{
    public class Opgave
    {
        public int Opgave_Id { get; set; }

        public string Navn { get; set; }

        public string Beskrivelse { get; set; }

        public int Status_Id { get; set; }

        public string Status { get; set; }

        public int Team_Id { get; set; }

        public string Team { get; set; }

        public int LedigeVagter { get; set; }
    }
}
