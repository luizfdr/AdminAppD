using Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ps_api_test_2.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        public static List<Group> gpList = readFileGroups();
        public static List<PostIt> psList = readFilePostIt();

        // GET: api/<ValuesController>
        [HttpGet]
        public List<Group> Get()
        {
            gpList = readFileGroups();
            return gpList;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}", Name = "GetGroups")]
        public IActionResult Get(int id)
        {
            try
            {
                gpList = readFileGroups();
                Group gp = gpList.Find(g => g.id == id);

                if (gp == null)
                {
                    return NotFound();
                }

                return Ok(gp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public Group Post([FromBody] Group newGroup)
        {
            gpList = readFileGroups();

            Boolean blOriginalName = true;
            foreach (Group gp in gpList)
            {
                if (gp.name == newGroup.name)
                {
                    blOriginalName = false;
                }
            }
            if (blOriginalName == true)
            {
                gpList.Add(newGroup);
                writeFileGroups(gpList);
                return newGroup;
            }
            else
            {
                return new Group(-1, "notOriginalName", true);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            //Can't modify groups so no code here :)
            //Yet...
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            gpList = readFileGroups();
            psList = readFilePostIt();
            Group gp = gpList.Find(g => g.id == id);
            gpList.Remove(gp);
            foreach(PostIt ps in psList)
            {
                if(ps.group == id)
                {
                    psList.Remove(ps);
                }
            }
            writeFilePostIt(psList);
            writeFileGroups(gpList);
        }

        //SPECIFIC-ACTION-METHODS

        [HttpGet("nextGroupId")]
        public int nextGroupId()
        {
            gpList = readFileGroups();
            return gpList[gpList.Count - 1].id + 1;
        }

        [HttpPost("userGroups")]
        public List<Group> ReturnUserGroups([FromBody] List<int> groupsList)
        {
            gpList = readFileGroups();
            List<Group> gpUserList = new List<Group>();
            foreach (int gpId in groupsList)
            {
                Group gp = gpList.Find(g => g.id == gpId);
                gpUserList.Add(gp);
            }
            return gpUserList;
        }

        //READ-WRITE-METHODS ****************************************************************
        public static List<Account> readFileAccount()
        {

            List<Account> acReadList = new List<Account>() { };
            string fp = "./data-files/accounts.json";
            try
            {
                string accounts = System.IO.File.ReadAllText(fp);
                Console.WriteLine(accounts);
                acReadList = JsonSerializer.Deserialize<List<Account>>(accounts);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Data file not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading data: {ex.Message}");
            }
            return acReadList;
        }

        public static List<Group> readFileGroups()
        {

            List<Group> gpReadList = new List<Group>() { };
            string fp = "./data-files/groups.json";
            try
            {
                string Groups = System.IO.File.ReadAllText(fp);
                Console.WriteLine(Groups);
                gpReadList = JsonSerializer.Deserialize<List<Group>>(Groups);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Data file not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading data: {ex.Message}");
            }
            return gpReadList;
        }

        public static List<PostIt> readFilePostIt()
        {
            List<PostIt> psReadList = new List<PostIt>() { };
            string fp = "./data-files/post-its.json";
            try
            {
                string PostIts = System.IO.File.ReadAllText(fp);
                Console.WriteLine(PostIts);
                psReadList = JsonSerializer.Deserialize<List<PostIt>>(PostIts);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Data file not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading data: {ex.Message}");
            }
            return psReadList;
        }

        public static void writeFileAccount(List<Account> accountList)
        {
            string fp = "./data-files/accounts.json";
            try
            {
                // Serialize the list of accounts to a JSON string
                string accountsUpdated = JsonSerializer.Serialize(accountList);

                // Write the JSON string to the file, overwriting its contents
                System.IO.File.WriteAllText(fp, accountsUpdated);

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

        public static void writeFilePostIt(List<PostIt> PostItList)
        {
            string fp = "./data-files/post-its.json";

            try
            {
                // Serialize the list of PostIts to a JSON string
                string PostItsUpdated = JsonSerializer.Serialize(PostItList);

                // Write the JSON string to the file, overwriting its contents
                System.IO.File.WriteAllText(fp, PostItsUpdated);

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

        public static void writeFileGroups(List<Group> GroupList)
        {
            string fp = "./data-files/groups.json";

            try
            {
                string GroupsUpdated = JsonSerializer.Serialize(GroupList);

                System.IO.File.WriteAllText(fp, GroupsUpdated);

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
