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
    public class KompetenceController : ControllerBase
    {
        private string connString = "User ID=systemadmin;Password=Festival987;Host=jadafestival-db.postgres.database.azure.com;Port=5432;Database=postgres;";

        // Hent alle kompetencer
        [HttpGet("all")]
        public async Task<IEnumerable<Kompetence>> HentAlle()
        {
            var sql = "SELECT kompetence_id, kompetence AS kompetence_navn FROM kompetence;";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var kompetenceListe = await connection.QueryAsync<Kompetence>(sql);

                    return kompetenceListe;
                }
            }
            catch (System.Exception)
            {
                return new List<Kompetence>();
            }
        }


       [HttpGet("brugerkompetence")]
        public async Task<IEnumerable<Kompetence>> HentKompetencer(int bruger_id, int kompetence_id)
        {
            var sql = $"SELECT * FROM hent_kompetencer({bruger_id}, {kompetence_id})";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var kompetenceListe = await connection.QueryAsync<Kompetence>(sql);

                    return kompetenceListe;
                }
            }
            catch (System.Exception)
            {
                return new List<Kompetence>();
            }
        }


    }
}
