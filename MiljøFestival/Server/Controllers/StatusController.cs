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
    public class StatusController : ControllerBase
    {
        private string connString;

        public StatusController(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("miljøDB");
        }

        // Hent alle statusser
        [HttpGet("all")]
        public async Task<IEnumerable<string>> HentAlle()
        {
            var querySQL = "SELECT status FROM status;";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var statusListe = await connection.QueryAsync<string>(querySQL);

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
