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
    public class RolleController : ControllerBase
    {
        private string connString;

        public RolleController(IConfiguration configuration)
        {
            connString = configuration.GetConnectionString("miljøDB");
        }

        // Hent alle roller
        [HttpGet("all")]
        public async Task<IEnumerable<string>> HentAlle()
        {
            var querySQL = "SELECT rolle FROM rolle;";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var rolleListe = await connection.QueryAsync<string>(querySQL);

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
