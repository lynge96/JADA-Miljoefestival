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
    public class VagtController : ControllerBase
    {
        private string connString;

        public VagtController(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("miljøDB");
        }

        // Hent alle vagter
        [HttpGet("all")]
        public async Task<IEnumerable<Vagt>> GetAll()
        {
            
            var sql = "SELECT vagt_id, start, slut, (bruger.fornavn || ' ' || bruger.efternavn) AS taget_af, bruger_id, opgave.navn AS opgave, opgave_id, opgave.beskrivelse, status.status FROM vagt LEFT JOIN bruger USING(bruger_id)  LEFT JOIN opgave USING(opgave_id) LEFT JOIN status USING(status_id);";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var vagtListe = await connection.QueryAsync<Vagt>(sql);

                    return vagtListe;
                }
            }
            catch (System.Exception)
            {
                return new List<Vagt>();
            }
        }


        // Find vagter efter bruger id
        [HttpGet("brugerVagter")]
        public async Task<IEnumerable<Vagt>> BrugerIdVagt(string brugerid) // Ændre til int bruger_id
        {
            var sql = $"SELECT vagt_id, start, slut, (bruger.fornavn || ' ' || bruger.efternavn) AS taget_af, bruger_id, opgave.navn AS opgave, opgave_id, opgave.beskrivelse, status.status FROM vagt LEFT JOIN bruger USING(bruger_id ) LEFT JOIN opgave USING(opgave_id) LEFT JOIN status USING(status_id) WHERE bruger_id = {brugerid};";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var vagtListe = await connection.QueryAsync<Vagt>(sql);

                    return vagtListe;
                }
            }
            catch (System.Exception)
            {
                return new List<Vagt>();
            }
        }


        // Find alle ledige vagter baseret på team ID
        [HttpGet("ledigeVagter")]
        public async Task<IEnumerable<Vagt>> LedigeVagter(int team_id)
        {
            var sql = $"SELECT vagt_id, start, slut, (bruger.fornavn || ' ' || bruger.efternavn) AS taget_af, bruger_id, opgave.navn AS opgave, opgave_id, opgave.beskrivelse, status.status FROM vagt LEFT JOIN bruger USING(bruger_id ) LEFT JOIN opgave USING(opgave_id ) LEFT JOIN status USING(status_id) WHERE bruger_id IS NULL AND opgave.team_id = {team_id};"; 

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var vagtListe = await connection.QueryAsync<Vagt>(sql);

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
            var sql = $"UPDATE vagt SET bruger_id = {vagt.Bruger_Id} WHERE vagt_id = {vagt.Vagt_Id};";

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


        // Fjerner vagt fra brugeren og gør den ledig til andre
        [HttpPut("frigivVagt")]
        public async Task FrigivVagt(Vagt vagt)
        {
            var sql = $"UPDATE vagt SET bruger_id = null WHERE vagt_id = {vagt.Vagt_Id};";

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


        // Sletter vagten
        [HttpPut("sletVagt")]
        public async Task SletVagt(Vagt vagt)
        {
            var sql = $"DELETE FROM vagt WHERE vagt_id = {vagt.Vagt_Id};";

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

        // Opdaterer vagt
        [HttpPut("opdaterVagt")]
        public async Task OpdaterBruger(Vagt vagt)
        {
            var sql = $"UPDATE vagt SET start = '{vagt.Start}', slut = '{vagt.Slut}' where vagt_id = '{vagt.Vagt_Id}'";

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


        // Opret en vagt
        [HttpPost("opret")]
        public async Task OpretVagt(Vagt vagt)
        {
            string vagtStart = vagt.Start.ToString("MM-dd-yyyy HH:mm:ss");
            string vagtSlut = vagt.Slut.ToString("MM-dd-yyyy HH:mm:ss");


            var sql = $"INSERT INTO vagt (start, slut, opgave_id) VALUES ('{vagtStart}', '{vagtSlut}', {vagt.Opgave_Id})";

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
