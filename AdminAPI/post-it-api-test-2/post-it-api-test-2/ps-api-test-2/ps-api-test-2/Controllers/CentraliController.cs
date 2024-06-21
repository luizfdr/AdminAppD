using Microsoft.AspNetCore.Mvc;
using Interfaces;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ps_api_test_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentraliController : ControllerBase
    {
        public static List<centrale> centraliList = ReadFileCentrali();


        // GET: api/<ValuesController>
        [HttpGet]
        public List<centrale> Get()
        {
            centraliList = ReadFileCentrali();
            return centraliList;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public centrale Get(int id)
        {
            centrale cs = null;
            for (int i = 0; i < centraliList.Count; i++)
            {
                
                if (id == centraliList[i].id)
                {
                    cs = centraliList[i];
                    return cs;
                }
            }

            //cs = new centrale();
            return cs;
            
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] centrale cs)
        {
            centraliList = ReadFileCentrali();
            centraliList.Add(cs);
            writeFileCentrali(centraliList);
            
        }

    
 
        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] centrale cs)
        {
            for (int i = 0; i < centraliList.Count; i++)
            {

                if (id == centraliList[i].id)
                {
                    centraliList[i] = cs;


                }
            }
            writeFileCentrali(centraliList);

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            for (int i = 0; i < centraliList.Count; i++)
            { 
                if (id == centraliList[i].id)
                {
                    centraliList.Remove(centraliList[i]);
                    writeFileCentrali(centraliList);
                    return true;
                }
            }
            return false;

        }
        public static List<centrale> ReadFileCentrali()
        {
            List<centrale> centraliList = new List<centrale>();
            string fp ="C:/Users/stede/Downloads/post-it-api-test-2/post-it-api-test-2/ps-api-test-2/ps-api-test-2/data-files/centrali.json";
            try
            {
                string centrali = System.IO.File.ReadAllText(fp);
                centraliList = JsonSerializer.Deserialize<List<centrale>>(centrali);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File non trovato");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore {ex.Message}");
            }
            for(int i=0;i<centraliList.Count;i++)
            {
                Console.WriteLine(centraliList[i]);
            }
            return centraliList;
        }
        public static void writeFileCentrali(List<centrale> centraliList)
        {
            string fp ="C:/Users/stede/Downloads/post-it-api-test-2/post-it-api-test-2/ps-api-test-2/ps-api-test-2/data-files/centrali.json";

            try
            {
                string centraliJson = JsonSerializer.Serialize(centraliList);
                System.IO.File.WriteAllText(fp, centraliJson);
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
