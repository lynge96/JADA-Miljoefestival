using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiljøFestival.Shared.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiljøFestival.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpgaveController : ControllerBase
    {

        private string connString = "User ID=systemadmin;Password=Festival987;Host=jadafestival-db.postgres.database.azure.com;Port=5432;Database=postgres;";


        // Henter alle opgaver
        [HttpGet("all")]
        public async Task<IEnumerable<Opgave>> GetAll()
        {
            var sql = "SELECT opgave_id, navn, beskrivelse, status_id, status.status AS status, team_id, team.team AS team FROM opgave LEFT JOIN status USING (status_id) LEFT JOIN team USING (team_id);";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var opgaveListe = await connection.QueryAsync<Opgave>(sql);

                    return opgaveListe;
                }
            }
            catch (System.Exception)
            {
                return new List<Opgave>();
            }
        }


        // Opdater en opgave
        [HttpPost("opdaterOpgave")]
        public async Task OpdaterOpgave(Opgave opgave)
        {
            var sql = $"CALL opdater_opgave_status({opgave.Opgave_Id}, '{opgave.Status}')";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    await connection.ExecuteAsync(sql);
                }
            }
            catch (System.Exception)
            {
                throw;  
            }

        }

        // Opret en opgave
        [HttpPost("opret")]
        public async Task OpretOpgave(Opgave opgave)
        {
            var sql = $"CALL opret_opgave('{opgave.Navn}', '{opgave.Beskrivelse}', '{opgave.Status}', '{opgave.Team}')";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    await connection.ExecuteAsync(sql);
                }
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        // Slet opgave
        [HttpPost("slet")]
        public async Task SletOpgave(Opgave opgave)
        {
            var sql = $"CALL slet_opgave({opgave.Opgave_Id});";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    await connection.ExecuteAsync(sql);
                }
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}
