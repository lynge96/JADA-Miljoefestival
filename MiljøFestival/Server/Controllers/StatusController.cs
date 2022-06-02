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
    public class StatusController : ControllerBase
    {
        private string connString;


        // Henter connectionstring fra appsettings.json
        public StatusController(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("miljøDB");
        }

        // Henter alle statusser
        [HttpGet("all")]
        public async Task<IEnumerable<string>> HentAlle()
        {
            var sql = "SELECT status FROM status;";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var statusListe = await connection.QueryAsync<string>(sql);

                    return statusListe;
                }
            }
            catch (System.Exception)
            {
                return new List<string>();
            }
        }
    }
}
