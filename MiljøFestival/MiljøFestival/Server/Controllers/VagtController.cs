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
    public class VagtController : ControllerBase
    {
        // Hent alle vagter
        [HttpGet("all")]
        public async Task<IEnumerable<Vagt>> GetAll()
        {
            
            var connString = "User ID=jbpzuakg;Password=7FunsLh3XcgblqOlN4WJ5dIMJr2v134O;Host=abul.db.elephantsql.com;Port=5432;Database=jbpzuakg;";

            var sql = "SELECT vagt.vagt_id, vagt.start, vagt.slut, bruger.fornavn AS taget_af, opgave.type AS opgave_navn, vagt.bruger_id, vagt.opgave_id FROM vagt LEFT JOIN bruger USING(bruger_id ) LEFT JOIN opgave USING(opgave_id )";

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
        public async Task<IEnumerable<Vagt>> BrugerIdVagt(string brugerid)
        {

            var connString = "User ID=jbpzuakg;Password=7FunsLh3XcgblqOlN4WJ5dIMJr2v134O;Host=abul.db.elephantsql.com;Port=5432;Database=jbpzuakg;";

            var sql = $"SELECT vagt.vagt_id, vagt.start, vagt.slut, bruger.fornavn AS taget_af, opgave.type AS opgave_navn, vagt.bruger_id, vagt.opgave_id FROM vagt LEFT JOIN bruger USING(bruger_id ) LEFT JOIN opgave USING(opgave_id ) WHERE bruger.bruger_id = {brugerid}";

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


        // Find alle ledige vagter
        [HttpGet("ledigeVagter")]
        public async Task<IEnumerable<Vagt>> LedigeVagter()
        {

            var connString = "User ID=jbpzuakg;Password=7FunsLh3XcgblqOlN4WJ5dIMJr2v134O;Host=abul.db.elephantsql.com;Port=5432;Database=jbpzuakg;";

            var sql = $"SELECT vagt.vagt_id, vagt.start, vagt.slut, bruger.fornavn AS taget_af, opgave.type AS opgave_navn, vagt.bruger_id, vagt.opgave_id FROM vagt LEFT JOIN bruger USING(bruger_id ) LEFT JOIN opgave USING(opgave_id )WHERE bruger_id IS NULL;";

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
        [HttpPost("tagVagt")]
        public async Task TagVagt(Vagt vagt)
        {
            var connString = "User ID=jbpzuakg;Password=7FunsLh3XcgblqOlN4WJ5dIMJr2v134O;Host=abul.db.elephantsql.com;Port=5432;Database=jbpzuakg;";

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
        [HttpPost("frigivVagt")]
        public async Task FrigivVagt(Vagt vagt)
        {
            var connString = "User ID=jbpzuakg;Password=7FunsLh3XcgblqOlN4WJ5dIMJr2v134O;Host=abul.db.elephantsql.com;Port=5432;Database=jbpzuakg;";

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
        [HttpPost("sletVagt")]
        public async Task SletVagt(Vagt vagt)
        {
            var connString = "User ID=jbpzuakg;Password=7FunsLh3XcgblqOlN4WJ5dIMJr2v134O;Host=abul.db.elephantsql.com;Port=5432;Database=jbpzuakg;";

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
        [HttpPost("opdaterVagt")]
        public async Task OpdaterBruger(Vagt vagt)
        {
            var connString = "User ID=jbpzuakg;Password=7FunsLh3XcgblqOlN4WJ5dIMJr2v134O;Host=abul.db.elephantsql.com;Port=5432;Database=jbpzuakg;";

            var sql = $"UPDATE vagt SET start = @start, slut = @slut where vagt_id = @vagt_id";

            var queryArguments = new
            {
                start = vagt.Start,
                slut = vagt.Slut,
                vagt_id = vagt.Vagt_Id
                
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
