using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MiljøFestival.Shared.Models;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiljøFestival.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KompetenceController : ControllerBase
    {
        private string connString;

        // Henter connectionstring fra appsettings.json
        public KompetenceController(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("miljøDB");
        }

        // Henter alle kompetencer
        [HttpGet("all")]
        public async Task<IEnumerable<Kompetence>> HentAlle()
        {
            var querySQL = "SELECT * FROM alle_kompetencer";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var kompetenceListe = await connection.QueryAsync<Kompetence>(querySQL);

                    return kompetenceListe;
                }
            }
            catch (System.Exception)
            {
                return new List<Kompetence>();
            }
        }

        // Henter de kompetencer, som brugeren allerede har valgt til
       [HttpGet("brugerkompetence")]
        public async Task<IEnumerable<Kompetence>> HentKompetencer(int bruger_id, int kompetence_id)
        {
            var querySQL = $"SELECT * FROM hent_kompetencer({bruger_id}, {kompetence_id})";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var kompetenceListe = await connection.QueryAsync<Kompetence>(querySQL);

                    return kompetenceListe;
                }
            }
            catch (System.Exception)
            {
                return new List<Kompetence>();
            }
        }


        // Henter de kompetencer, som brugeren allerede har valgt til (kun med bruger_id)
        [HttpGet("brugerIDkompetence")]
        public async Task<IEnumerable<Kompetence>> HentKompetencer(int bruger_id)
        {
            var querySQL = $"SELECT * FROM hent_bruger_kompetence({bruger_id})";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var kompetenceListe = await connection.QueryAsync<Kompetence>(querySQL);

                    return kompetenceListe;
                }
            }
            catch (System.Exception)
            {
                return new List<Kompetence>();
            }
        }

        // Opdaterer kompetencevalget for en bruger
        [HttpPut("opdater")]
        public async Task OpretBruger(List<Kompetence> kompetenceListe)
        {
            foreach (var kompetence in kompetenceListe)
            {
                var querySQL = $"CALL opdater_kompetencer({kompetence.Bruger_Id}, {kompetence.Kompetence_Id}, {kompetence.IsChecked});";


                using (var connection = new NpgsqlConnection(connString))
                {
                    await connection.ExecuteAsync(querySQL);
                }
            }
        }
    }
}
