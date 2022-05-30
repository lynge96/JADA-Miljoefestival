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
        private string connString; 

        public BrugerController(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("miljøDB");
        }

        // Hent alle brugere
        [HttpGet("all")]
        public async Task<IEnumerable<Bruger>> HentAlle()
        {
            var sql = "SELECT * FROM alle_brugere;";

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
            var sql = $"CALL opret_bruger('{bruger.Fornavn}', '{bruger.Efternavn}', '{bruger.Telefon}', '{bruger.Email}', '{bruger.Adresse}', '{bruger.Rolle}', '{bruger.Team}', '{bruger.Kode}');";

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
            var sql = $"SELECT * FROM hent_bruger_brugerID({bruger_id})";

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
        [HttpPut("opdater")]
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


        // Opdaterer oplysninger for frivillige fra koordinator/formandens-frivilligeside
        [HttpPut("opdaterFrivillig")]
        public async Task OpdaterFrivillig(Bruger bruger)
        {
            var sql = $"CALL opdater_frivillig({bruger.Bruger_Id}, '{bruger.Rolle}', '{bruger.Team}')";

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

        // Opdaterer brugerens password.
        [HttpPut("opdaterPassword")]
        public async Task OpdaterPassword(Bruger bruger)
        {
            var sql = $"CALL opdater_password('{bruger.Bruger_Id}', '{bruger.Kode}')";

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


        // Find bruger_id med email
        [HttpGet("findBrugerIdMail")]
        public async Task<IEnumerable<int>> HentBrugerIdMail(string email)
        {
            var sql = $"SELECT bruger_id FROM bruger WHERE email = '{email}'";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var bruger_id = await connection.QueryAsync<int>(sql);

                    return bruger_id;
                }
            }
            catch (System.Exception)
            {
                return new List<int>();
            }

        }
    }
}
