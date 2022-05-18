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

        // Henter alle opgaver
        [HttpGet("all")]
        public async Task<IEnumerable<Opgave>> GetAll()
        {

            var connString = "User ID=jbpzuakg;Password=7FunsLh3XcgblqOlN4WJ5dIMJr2v134O;Host=abul.db.elephantsql.com;Port=5432;Database=jbpzuakg;";

            var sql = "SELECT opgave_id, status, type, beskrivelse FROM opgave";

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
            var connString = "User ID=jbpzuakg;Password=7FunsLh3XcgblqOlN4WJ5dIMJr2v134O;Host=abul.db.elephantsql.com;Port=5432;Database=jbpzuakg;";

            var sql = $"UPDATE opgave SET status = '{opgave.Status}' WHERE opgave_id = '{opgave.Opgave_Id}'";

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
            var connString = "User ID=jbpzuakg;Password=7FunsLh3XcgblqOlN4WJ5dIMJr2v134O;Host=abul.db.elephantsql.com;Port=5432;Database=jbpzuakg;";

            var sql = $"INSERT INTO opgave (status, type, beskrivelse) VALUES ('{opgave.Status}', '{opgave.Type}', '{opgave.Beskrivelse}')";

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
