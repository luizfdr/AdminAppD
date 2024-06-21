using Microsoft.AspNetCore.Mvc;
using Interfaces;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PostItApi_Test.Controllers
{
    [Route("api/postit")]
    [ApiController]
    public class PostItController : ControllerBase
    {

        public static List<Account> acList = readFileAccount();
        public static List<PostIt> psList = readFilePostIt();
        public static List<Group> gpList = readFileGroups();

        // GET: api/<ValuesController>
        [HttpGet]
        public List<PostIt> Get()
        {
            psList = readFilePostIt();
            return psList;
        }

        // GET api/PostIt/5
        [HttpGet("{id}", Name = "GetPostIts")]
        public IActionResult Get(int id)
        {
            try
            {
                psList = readFilePostIt();
                PostIt ps = psList.Find(p => p.id == id);

                if (ps == null)
                {
                    // Return a 404 Not Found response if the account is not found.
                    return NotFound();
                }

                // Return a 200 OK response with the account data as JSON.
                return Ok(ps);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a 500 Internal Server Error response.
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] PostIt ps)
        {
            psList = readFilePostIt();
            psList.Add(ps);
            writeFilePostIt(psList);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public List<PostIt> Put(int id, [FromBody] PostIt ps)
        {
            psList = readFilePostIt();
            PostIt psToUpdate = psList.Find(p => p.id == id);
            int index = psList.IndexOf(psToUpdate);

            psList[index].versions.Add(ps.versions[ps.versionId-1]);

            psList[index].versionId = ps.versionId;

            writeFilePostIt(psList);

            return psList;
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            psList = readFilePostIt();
            PostIt ps = psList.Find(p => p.id == id);
            psList.Remove(ps);
            writeFilePostIt(psList);
        }

        //SPECIFIC-DATA-METHODS

        [HttpPost("userPosts")]
        public List<PostIt> ReturnUserPosts([FromBody] int userId)
        {
            psList = readFilePostIt();
            List<PostIt> psUserList = new List<PostIt>();
            foreach (PostIt ps in psList)
            {
                if (ps.userId == userId)
                {
                    psUserList.Add(ps);
                }
            }
            return psUserList;
        }

        [HttpPost("followedPosts")]
        public List<PostIt> ReturnFollowedPosts([FromBody] int userId)
        {
            acList = readFileAccount();
            psList = readFilePostIt();

            Account currentUser = acList.Find(a => a.id == userId);
            List<PostIt> psUserList = new List<PostIt>();
            foreach (PostIt ps in psList)
            {
                foreach (int followedUser in currentUser.followedUsers)
                {
                    if (followedUser == ps.userId)
                    {
                        psUserList.Add(ps);
                    }
                }
            }
            return psUserList;
        }

        [HttpPost("groupPosts")]
        public List<PostIt> ReturnGroupPosts([FromBody] int groupId)
        {
            psList = readFilePostIt();
            List<PostIt> psUserList = new List<PostIt>();
            foreach (PostIt ps in psList)
            {
                if (ps.group == groupId)
                {
                    psUserList.Add(ps);
                }
            }
            return psUserList;
        }

        [HttpGet("nextPostId")]
        public int nextPostId()
        {
            psList = readFilePostIt();
            if (psList.Count > 0)
            {
                return psList[psList.Count - 1].id + 1;
            }
            else
            {
                return 1;
            }
        }

        //READ-WRTE-METHODS ****************************************************************
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
