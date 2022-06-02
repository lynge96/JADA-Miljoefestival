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
    public class BrugerController : ControllerBase
    {
        private string connString; 

        // Henter connectionstring fra appsettings.json
        public BrugerController(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("miljøDB");
        }

        // Henter en liste med alle brugere
        [HttpGet("all")]
        public async Task<IEnumerable<Bruger>> HentAlle()
        {
            var querySQL = "SELECT * FROM alle_brugere;";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var brugerListe = await connection.QueryAsync<Bruger>(querySQL);

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
            var querySQL = $"CALL opret_bruger('{bruger.Fornavn}', '{bruger.Efternavn}', '{bruger.Telefon}', '{bruger.Email}', '{bruger.Adresse}', '{bruger.Rolle}', '{bruger.Team}', '{bruger.Kode}');";
 
            
            using (var connection = new NpgsqlConnection(connString))
            {
                await connection.ExecuteAsync(querySQL);
            }
        }


        // Tjekker om email og kode tastet ind er korrekt og om en bruger er oprettet. Henter brugeren herefter
        [HttpGet("tjeklogin")]
        public async Task<IEnumerable<Bruger>> TjekLogin(string email, string kode)
        {
            var querySQL = $"SELECT * FROM tjek_login('{email}', '{kode}');";


            using (var connection = new NpgsqlConnection(connString))
            {
                var test = await connection.QueryAsync<Bruger>(querySQL);

                return test;
            }
        }


        // Opdaterer oplysninger for brugers profil
        [HttpPut("opdater")]
        public async Task OpdaterBruger(Bruger bruger)
        {
            var querySQL = $"CALL opdater_bruger({bruger.Bruger_Id}, '{bruger.Fornavn}', '{bruger.Efternavn}', '{bruger.Telefon}', '{bruger.Email}', '{bruger.Adresse}', '{bruger.Rolle}', '{bruger.Team}')";


            using (var connection = new NpgsqlConnection(connString))
            {
                await connection.ExecuteAsync(querySQL);
            }
        }

        
        // Opdaterer oplysninger for frivillige fra koordinator/formandens-frivilligeside
        [HttpPut("opdaterFrivillig")]
        public async Task OpdaterFrivilligBruger(Bruger bruger)
        {
            var querySQL = $"CALL opdater_frivillig({bruger.Bruger_Id}, '{bruger.Rolle}', '{bruger.Team}')";


            using (var connection = new NpgsqlConnection(connString))
            {
                await connection.ExecuteAsync(querySQL);
            }
        }

        // Opdaterer brugerens password.
        [HttpPut("opdaterPassword")]
        public async Task OpdaterPassword(Bruger bruger)
        {
            var querySQL = $"CALL opdater_password('{bruger.Bruger_Id}', '{bruger.Kode}')";


            using (var connection = new NpgsqlConnection(connString))
            {
                await connection.ExecuteAsync(querySQL);
            }
        }


        // Sletter brugerens profil
        [HttpPut("sletProfil")]
        public async Task SletProfil(Bruger bruger)
        {
            var querySQL = $"CALL slet_profil({bruger.Bruger_Id})";


            using (var connection = new NpgsqlConnection(connString))
            {
                await connection.ExecuteAsync(querySQL);
            }
        }

        
        // Find bruger_id med email
        [HttpGet("findBrugerIdMail")]
        public async Task<IEnumerable<int>> HentBrugerIdMail(string email)
        {
            var querySQL = $"SELECT bruger_id FROM bruger WHERE email = '{email}'";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var bruger_id = await connection.QueryAsync<int>(querySQL);

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
