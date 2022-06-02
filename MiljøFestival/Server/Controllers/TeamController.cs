using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MiljøFestival.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {
        private string connString;
        public TeamController(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("miljøDB");
        }

        // Hent alle teams
        [HttpGet("all")]
        public async Task<IEnumerable<string>> HentAlle()
        {
            var querySQL = "SELECT team FROM team;";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var teamListe = await connection.QueryAsync<string>(querySQL);

                    return teamListe;
                }
            }
            catch (System.Exception)
            {
                return new List<string>();
            }
        }
    }
}
