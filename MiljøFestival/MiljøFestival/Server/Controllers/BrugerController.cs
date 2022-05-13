﻿using Dapper;
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


        // Tjek om login er korret
        [HttpGet("tjeklogin")]
        public async Task<IEnumerable<string>> TjekLogin(string email, string kode)
        {
            var connString = "User ID=jbpzuakg;Password=7FunsLh3XcgblqOlN4WJ5dIMJr2v134O;Host=abul.db.elephantsql.com;Port=5432;Database=jbpzuakg;";

            var sql = $"SELECT tjeklogin('{email}', '{kode}');";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    var test = await connection.QueryAsync<string>(sql);

                    return test;
                }
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}
