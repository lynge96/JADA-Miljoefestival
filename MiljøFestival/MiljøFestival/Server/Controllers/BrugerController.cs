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
    public class BrugerController : ControllerBase
    {
        private string connString = "User ID=systemadmin;Password=Festival987;Host=jadafestival-db.postgres.database.azure.com;Port=5432;Database=postgres;";

        // Hent alle brugere
        [HttpGet("all")]
        public async Task<IEnumerable<Bruger>> HentAlle()
        {
            var sql = "SELECT bruger_id, fornavn, efternavn, telefon, email, adresse, rolle_id, team_id, rolle.rolle AS rolle, team.team AS team FROM bruger LEFT JOIN rolle USING (rolle_id) LEFT JOIN team USING (team_id);";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var brugerListe = await connection.QueryAsync<Bruger>(sql);

                    return brugerListe;
                }
            }
            catch (System.Exception)
            {
                return new List<Bruger>();
            }
        }


        // Opret en bruger
        [HttpPost("opret")]
        public async Task OpretBruger(Bruger bruger)
        {
            var sql = $"CALL opret_bruger('{bruger.Fornavn}', '{bruger.Efternavn}', '{bruger.Telefon}', '{bruger.Email}', '{bruger.Adresse}', '{bruger.Rolle}', '{bruger.Team}', '{bruger.Kode}')";

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


        // Tjekker om email og kode tastet ind er korrekt og om en bruger er oprettet. Henter brugeren herefter
        [HttpGet("tjeklogin")]
        public async Task<IEnumerable<Bruger>> TjekLogin(string email, string kode)
        {
            var sql = $"SELECT * FROM tjek_login('{email}', '{kode}');";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var test = await connection.QueryAsync<Bruger>(sql);

                    return test;
                }
            }
            catch (System.Exception)
            {
                throw;
            }

        }


        // Find bruger med Bruger Id
        [HttpGet("findBrugerId")]
        public async Task<IEnumerable<Bruger>> HentBrugerBrugerId(int bruger_id)
        {
            var sql = $"SELECT bruger_id, fornavn, efternavn, telefon, email, adresse, rolle_id, team_id, rolle.rolle AS rolle, team.team AS team FROM bruger LEFT JOIN rolle USING (rolle_id) LEFT JOIN team USING (team_id) WHERE bruger_id = '{bruger_id}';";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var bruger = await connection.QueryAsync<Bruger>(sql);

                    return bruger;
                }
            }
            catch (System.Exception)
            {
                return new List<Bruger>();
            }

        }


        // Opdaterer oplysninger for brugers profil
        [HttpPost("opdater")]
        public async Task OpdaterBruger(Bruger bruger)
        {
            var sql = $"CALL opdater_bruger({bruger.Bruger_Id}, '{bruger.Fornavn}', '{bruger.Efternavn}', '{bruger.Telefon}', '{bruger.Email}', '{bruger.Adresse}', '{bruger.Rolle}', '{bruger.Team}')";

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
