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
        [HttpGet("all")]
        public async Task<IEnumerable<Vagt>> GetAll()
        {
            
            var connString = "User ID = jbpzuakg; Password = 7FunsLh3XcgblqOlN4WJ5dIMJr2v134O; Server = abul.db.elephantsql.com; Port = 5432; Database = jbpzuakg;";

            var sql = "SELECT vagt_id, start, slut, bruger_id, opgave_id FROM vagt";

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
    }
}
