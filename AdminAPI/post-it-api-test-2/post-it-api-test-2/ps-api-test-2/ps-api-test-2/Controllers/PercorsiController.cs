using Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ps_api_test_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PercorsiController : ControllerBase
    {
        static List<percorso> percorsoList = ReadFilePercorsi();





        // GET: api/<Percorsi7Controller>
        [HttpGet]
        public List<percorso> Get()
        {
            return percorsoList;
        }

        // GET api/<Percorsi7Controller>/5
        [HttpGet("{id}")]
        public percorso Get(int id)
        {
            percorso pr = null;
            for (int i = 0; i < percorsoList.Count; i++)
            {

                if (id == percorsoList[i].id)
                {
                    pr = percorsoList[i];
                    return pr;
                }
            }

            
            return pr;
        }

        // POST api/<Percorsi7Controller>
        [HttpPost]
        public void Post(percorso ps)
        {
            percorsoList = ReadFilePercorsi();
            percorsoList.Add(ps);
            WriteFilePercorsi(percorsoList);
        }

        // PUT api/<Percorsi7Controller>/5
        [HttpPut("{id}")]
        public bool Put(int id,percorso ps )
        {
            for(int i=0;i<percorsoList.Count;i++)
            {
                if (id == percorsoList[i].id)
                {
                    percorsoList[i] = ps;
                    return true;
                }
            }
            return false;
        }

        // DELETE api/<Percorsi7Controller>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            for (int i = 0; i < percorsoList.Count; i++)
            {
                if (id == percorsoList[i].id)
                {
                    percorsoList.Remove(percorsoList[i]);
                    WriteFilePercorsi(percorsoList);
                    return true;
                }
            }
            return false;
        }
        public static List<percorso> ReadFilePercorsi()
        {
            List<percorso> percorsoList = new List<percorso>();
            string fp = "C:\\Users\\stede\\Downloads\\post-it-api-test-2\\post-it-api-test-2\\ps-api-test-2\\ps-api-test-2\\data-files\\centrali.json\\";
            try
            {
                string percorsi = System.IO.File.ReadAllText(fp);
                percorsoList = JsonSerializer.Deserialize<List<percorso>>(percorsi);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File non trovato");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore {ex.Message}");
            }
            for (int i = 0; i < percorsoList.Count; i++)
            {
                Console.WriteLine(percorsoList[i]);
            }
            return percorsoList;
        }
        public static void WriteFilePercorsi(List<percorso> percorsiList)
        {
            string fp = "C:/Users/stede/Downloads/post-it-api-test-2/post-it-api-test-2/ps-api-test-2/ps-api-test-2/data-files/percorsi.json";

            try
            {
                string percorsiJson = JsonSerializer.Serialize(percorsiList);
                System.IO.File.WriteAllText(fp, percorsiJson);
                Console.WriteLine("JSON file has been overwritten successfully.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Data file not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to JSON file: {ex.Message}");
            }
        }
    }
}
