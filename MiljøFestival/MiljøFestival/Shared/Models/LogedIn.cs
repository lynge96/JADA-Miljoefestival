namespace MiljøFestival.Shared.Models
{
    public class LogedIn
    {
        public int? Bruger_Id { get; set; }

        public bool Koordinator { get; set; }

        public bool LoggetPå { get; set; }
    }
}
