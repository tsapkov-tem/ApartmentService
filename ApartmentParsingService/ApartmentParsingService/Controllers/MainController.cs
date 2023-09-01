using ApartmentParsingService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.OpenApi.Expressions;
using System.Text.RegularExpressions;

namespace ApartmentParsingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {

        DbService dbService = new DbService();


        [HttpPost("/Subscribe/link={link}&email={email}")]
        public string Subscribe(string link, string email)
        {
            link = link.Replace("%2F", "/");
            dbService.connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = dbService.connection;
            command.CommandText = string.Format("INSERT INTO Subscriptions (email, link) VALUES ('{0}','{1}');", email, link);
            command.ExecuteNonQuery();
            dbService.connection.Close();
            return link + " " + email;
        }

        [HttpGet("/GetPrices")]
        public HashSet<string> GetPrices()
        {
            var result = new HashSet<string>();
            dbService.connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = dbService.connection;
            command.CommandText = "SELECT * FROM Subscriptions";
            string link;

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())   
                    {
                        link = (string)reader.GetValue(2);
                        GetRequest request = new GetRequest(link + "?ajax=1&");
                        request.Run();
                        var response = request.Respose;
                        //"price":"9952000", запись цены в ответе
                        var rg = new Regex(@"price.:.(.*?).,");
                        result.Add(rg.Match(response).Groups[1].Value + " " + link);
                    }
                }
            }
            dbService.connection.Close();
            return result;
        }
    }
}
