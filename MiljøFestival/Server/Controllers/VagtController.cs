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
    public class VagtController : ControllerBase
    {
        private string connString;

        // Henter connectionstring fra appsettings.json
        public VagtController(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("miljøDB");
        }

        // Henter alle vagter
        [HttpGet("all")]
        public async Task<IEnumerable<Vagt>> GetAll()
        {
            
            var querySQL = "SELECT * FROM alle_vagter";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var vagtListe = await connection.QueryAsync<Vagt>(querySQL);

                    return vagtListe;
                }
            }
            catch (System.Exception)
            {
                return new List<Vagt>();
            }
        }


        // Finder vagter der tilhører en bruger. Tager bruger_id som input parameter
        [HttpGet("brugerVagter")]
        public async Task<IEnumerable<Vagt>> BrugerIdVagt(int brugerid)
        {
            var querySQL = $"SELECT * FROM hent_bruger_vagter({brugerid})";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var vagtListe = await connection.QueryAsync<Vagt>(querySQL);

                    return vagtListe;
                }
            }
            catch (System.Exception)
            {
                return new List<Vagt>();
            }
        }


        // Finder alle ledige vagter baseret på team_id som input parameter
        [HttpGet("ledigeVagter")]
        public async Task<IEnumerable<Vagt>> LedigeVagter(int team_id)
        {
            var querySQL = $"SELECT * FROM hent_ledige_vagter_team({team_id})"; 

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var vagtListe = await connection.QueryAsync<Vagt>(querySQL);

                    return vagtListe;
                }
            }
            catch (System.Exception)
            {
                return new List<Vagt>();
            }
        }


        // Tager en vagt 
        [HttpPut("tagVagt")]
        public async Task TagVagt(Vagt vagt)
        {
            var querySQL = $"UPDATE vagt SET bruger_id = {vagt.Bruger_Id} WHERE vagt_id = {vagt.Vagt_Id};";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    await connection.ExecuteAsync(querySQL);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }


        // Fjerner vagt fra brugeren og gør den ledig til andre
        [HttpPut("frigivVagt")]
        public async Task FrigivVagt(Vagt vagt)
        {
            var querySQL = $"UPDATE vagt SET bruger_id = null WHERE vagt_id = {vagt.Vagt_Id};";


            using (var connection = new NpgsqlConnection(connString))
            {
                await connection.ExecuteAsync(querySQL);
            }
        }


        // Sletter vagten
        [HttpPut("sletVagt")]
        public async Task SletVagt(Vagt vagt)
        {
            var querySQL = $"DELETE FROM vagt WHERE vagt_id = {vagt.Vagt_Id};";


            using (var connection = new NpgsqlConnection(connString))
            {
                await connection.ExecuteAsync(querySQL);
            }
        }

        // Opdaterer vagt
        [HttpPut("opdaterVagt")]
        public async Task OpdaterBruger(Vagt vagt)
        {
            string vagtStart = vagt.Start.ToString("MM-dd-yyyy HH:mm:ss");
            string vagtSlut = vagt.Slut.ToString("MM-dd-yyyy HH:mm:ss");

            var querySQL = $"UPDATE vagt SET start = '{vagtStart}', slut = '{vagtSlut}' where vagt_id = '{vagt.Vagt_Id}'";


            using (var connection = new NpgsqlConnection(connString))
            {
                await connection.ExecuteAsync(querySQL);
            }
        }


        // Opretter en vagt
        [HttpPost("opret")]
        public async Task OpretVagt(Vagt vagt)
        {
            string vagtStart = vagt.Start.ToString("MM-dd-yyyy HH:mm:ss");
            string vagtSlut = vagt.Slut.ToString("MM-dd-yyyy HH:mm:ss");

            var querySQL = $"INSERT INTO vagt (start, slut, opgave_id) VALUES ('{vagtStart}', '{vagtSlut}', {vagt.Opgave_Id})";


            using (var connection = new NpgsqlConnection(connString))
            {
                await connection.ExecuteAsync(querySQL);
            }
        }
    }
}
