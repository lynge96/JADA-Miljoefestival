using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

        private string connString;

        // Henter connectionstring fra appsettings.json
        public OpgaveController(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("miljøDB");
        }


        // Henter alle opgaver
        [HttpGet("all")]
        public async Task<IEnumerable<Opgave>> GetAll()
        {
            var sql = "SELECT * FROM alle_opgaver;";

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


        // Opdaterer en opgave
        [HttpPut("opdaterOpgave")]
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

        // Opretter en opgave
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

        // Sletter opgave
        [HttpPut("slet")]
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
