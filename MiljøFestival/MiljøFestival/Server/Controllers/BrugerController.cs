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

        // Hent alle brugere
        [HttpGet("all")]
        public async Task<IEnumerable<Bruger>> HentAlle()
        {
            var connString = "User ID=jbpzuakg;Password=7FunsLh3XcgblqOlN4WJ5dIMJr2v134O;Host=abul.db.elephantsql.com;Port=5432;Database=jbpzuakg;";

            var sql = "SELECT bruger_id, fornavn, efternavn, telefon, email, adresse, koordinator FROM bruger";

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
            var connString = "User ID=jbpzuakg;Password=7FunsLh3XcgblqOlN4WJ5dIMJr2v134O;Host=abul.db.elephantsql.com;Port=5432;Database=jbpzuakg;";

            var sql = $"CALL opret_bruger(@fornavn, @efternavn, @telefon, @email, @adresse, @adresse)";

            var queryArguments = new
            {
                fornavn = bruger.Fornavn,
                efternavn = bruger.Efternavn,
                telefon = bruger.Telefon,
                email = bruger.Email,
                adresse = bruger.Adresse,
                kode = bruger.Kode

            };

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    await connection.ExecuteAsync(sql, queryArguments);

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
            var connString = "User ID=jbpzuakg;Password=7FunsLh3XcgblqOlN4WJ5dIMJr2v134O;Host=abul.db.elephantsql.com;Port=5432;Database=jbpzuakg;";

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
            var connString = "User ID=jbpzuakg;Password=7FunsLh3XcgblqOlN4WJ5dIMJr2v134O;Host=abul.db.elephantsql.com;Port=5432;Database=jbpzuakg;";

            var sql = $"SELECT bruger_id, fornavn, efternavn, telefon, email, adresse, koordinator, hashpwd AS kode FROM bruger WHERE bruger_id = '{bruger_id}'";

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
        [HttpPost("opdaterBruger")]
        public async Task OpdaterBruger(Bruger bruger)
        {
            var connString = "User ID=jbpzuakg;Password=7FunsLh3XcgblqOlN4WJ5dIMJr2v134O;Host=abul.db.elephantsql.com;Port=5432;Database=jbpzuakg;";

            var sql = $"UPDATE bruger SET fornavn = @fornavn, efternavn = @efternavn, telefon = @telefon, email = @email, adresse = @adresse, koordinator = @koordinator WHERE bruger_id = @bruger_id,";

            var queryArguments = new
            {
                fornavn = bruger.Fornavn,
                efternavn = bruger.Efternavn,
                telefon = bruger.Telefon,
                email = bruger.Email,
                adresse = bruger.Adresse,
                bruger_id = bruger.Bruger_Id,
                koordinator = bruger.Koordinator
            };

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    await connection.ExecuteAsync(sql, queryArguments);
                }
            }
            catch (System.Exception)
            {
                throw;
            }



        }

    }
}
