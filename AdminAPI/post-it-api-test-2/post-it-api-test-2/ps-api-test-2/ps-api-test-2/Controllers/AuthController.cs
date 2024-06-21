using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ps_api_test_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        // GET: api/<AuthController>
        [HttpGet]
        public bool Get(string username,string password)
        {
            LogInfo utente= new LogInfo("s","s");
            string fp = "C:/Users/stede/Downloads/post-it-api-test-2/post-it-api-test-2/ps-api-test-2/ps-api-test-2/data-files/credenziali.json";
            try{
                string credenziali = System.IO.File.ReadAllText(fp);
                utente = JsonSerializer.Deserialize<LogInfo>(credenziali);
                if (utente.username == username && utente.password == password)
                { return true; }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File non trovato");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore {ex.Message}");
            }
            return false;
            
        }

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        { return "value";}

        // POST api/<AuthController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
