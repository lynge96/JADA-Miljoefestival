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
    public class RolleController : ControllerBase
    {
        private string connString = "User ID=systemadmin;Password=Festival987;Host=jadafestival-db.postgres.database.azure.com;Port=5432;Database=postgres;";

        // Hent alle teams
        [HttpGet("all")]
        public async Task<IEnumerable<string>> HentAlle()
        {
            var sql = "SELECT rolle FROM rolle;";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var rolleListe = await connection.QueryAsync<string>(sql);

                    return rolleListe;
                }
            }
            catch (System.Exception)
            {
                return new List<string>();
            }
        }


    }
}
