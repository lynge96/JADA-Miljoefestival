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
    public class OpgaveController : ControllerBase
    {

        private string connString;

        public OpgaveController(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("miljøDB");
        }


        // Henter alle opgaver
        [HttpGet("all")]
        public async Task<IEnumerable<Opgave>> GetAll()
        {
            var querySQL = "SELECT * FROM alle_opgaver;";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var opgaveListe = await connection.QueryAsync<Opgave>(querySQL);

                    return opgaveListe;
                }
            }
            catch (System.Exception)
            {
                return new List<Opgave>();
            }
        }


        // Opdater en opgave
        [HttpPut("opdaterOpgave")]
        public async Task OpdaterOpgave(Opgave opgave)
        {
            var querySQL = $"CALL opdater_opgave_status({opgave.Opgave_Id}, '{opgave.Status}')";


            using (var connection = new NpgsqlConnection(connString))
            {
                await connection.ExecuteAsync(querySQL);
            }
        }

        // Opret en opgave
        [HttpPost("opret")]
        public async Task OpretOpgave(Opgave opgave)
        {
            var querySQL = $"CALL opret_opgave('{opgave.Navn}', '{opgave.Beskrivelse}', '{opgave.Status}', '{opgave.Team}')";


            using (var connection = new NpgsqlConnection(connString))
            {
                await connection.ExecuteAsync(querySQL);
            }
        }

        // Slet opgave
        [HttpPut("slet")]
        public async Task SletOpgave(Opgave opgave)
        {
            var querySQL = $"CALL slet_opgave({opgave.Opgave_Id});";


            using (var connection = new NpgsqlConnection(connString))
            {
                await connection.ExecuteAsync(querySQL);
            }
        }
    }
}
