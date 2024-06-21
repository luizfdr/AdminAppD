using Microsoft.AspNetCore.Mvc;
using Interfaces;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Group = Interfaces.Group;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PostItApi_Test.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public static List<Account> acList = readFileAccount();
        public static List<PostIt> psList = readFilePostIt();
        public static List<Group> gpList = readFileGroups();

        // GET: api/accounts
        [HttpGet]
        public List<Account> Get()
        {
            acList = readFileAccount();
            List<Account> newAcList = new List<Account>();
            foreach (Account ac in acList)
            {
                Account acNew = ac;
                acNew.password = "";
                acNew.groups = new List<int>();
                newAcList.Add(acNew);
            }
            return newAcList;
        }

        // GET api/accounts/5
        [HttpGet("{id}", Name = "GetAccount")]
        public IActionResult Get(int id)
        {
            try
            {
                acList = readFileAccount();
                Account ac = acList.Find(a => a.id == id);

                if (ac == null)
                {
                    // Return a 404 Not Found response if the account is not found.
                    return NotFound();
                }

                // Return a 200 OK response with the account data as JSON.
                return Ok(ac);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a 500 Internal Server Error response.
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        // POST api/accounts
        [HttpPost]
        public IActionResult Post([FromBody] Account ac)
        {
            acList = readFileAccount();
            var userOriginal = true;
            foreach (Account acFromList in acList)
            {
                if(acFromList.username == ac.username)
                {
                    userOriginal = false;
                }
            }
            if (userOriginal)
            {
                acList.Add(ac);
                writeFileAccount(acList);
                return Ok(acList);
            }
            else
            {
                return Ok(new List<Account>());
            }
        }

        // PUT api/accounts/5
        [HttpPut("{id}")]
        public Account Put([FromBody] Account ac)
        {
            acList = readFileAccount();
            Account acToUpdate = acList.Find(a => a.id == ac.id);
            int index = acList.IndexOf(acToUpdate);
            acList[index].followedUsers = ac.followedUsers;
            acList[index].pfp = ac.pfp;
            acList[index].groups = ac.groups;

            writeFileAccount(acList);

            return acList[index];
        }

        // DELETE api/accounts/5
        [HttpDelete("{id}")]
        public List<Account> Delete(int id)
        {
            acList = readFileAccount();
            psList = readFilePostIt();
            gpList = readFileGroups();
            Account ac = acList.Find(a => a.id == id);
            acList.Remove(ac);
            foreach (Account acFollowRemove in acList)
            {
                if(acFollowRemove.followedUsers.Contains(ac.id))
                {
                    acFollowRemove.followedUsers.Remove(ac.id);
                }
            }
            foreach (PostIt ps in psList)
            {
                if (ps.userId == ac.id)
                {
                    psList.Remove(ps);
                }
                else
                {
                    if (ps.likes.Contains(ac.id))
                    {
                        ps.likes.Remove(ac.id);
                    }
                }
            }

            writeFileAccount(acList);

            foreach (Group gp in gpList)
            {
                bool groupEmpty = true;
                foreach (Account acGroupCheck in acList)
                {
                    if (acGroupCheck.groups.Contains(gp.id) && acGroupCheck.id != 0)
                    {
                        groupEmpty = false;
                    }
                }
                if (groupEmpty == true)
                {
                    acList.Find(admin => admin.id == 0).groups.Remove(gp.id);
                    gpList.Remove(gp);
                    foreach (PostIt ps in psList)
                    {
                        if (ps.group == gp.id)
                        {
                            psList.Remove(ps);
                        }
                    }
                }
            }
            writeFileGroups(gpList);
            writeFilePostIt(psList);
            return acList;
        }

        //SPECIAL ACTIONS **********************************************************

        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] AccountLogInfo ac)
        {
            acList = readFileAccount();
            var account = acList.FirstOrDefault(a => a.username == ac.username);
            if (account != null)
            {
                if (account.password == ac.password)
                {
                    return Ok(account);
                }
                else
                {
                    var acWrongPassword = new Account("", -1, "", "", new List<int>(), new List<int>());
                    return Ok(acWrongPassword);
                }
            }
            else
            {
                var acWrongUsername = new Account("", -2, "", "", new List<int>(), new List<int>());
                return Ok(acWrongUsername);
            }
        }

        [HttpPost("editAccountName")]
        public Account editAccountName([FromBody] AccountLogInfo newAcUsername)
        {
            acList = readFileAccount();
            bool originalUsername = true;
            foreach (Account ac in acList)
            {
                if (ac.username == newAcUsername.username)
                {
                    originalUsername = false;
                }
            }

            if (originalUsername == true)
            {
                acList.Find(a => a.id == newAcUsername.id).username = newAcUsername.username;
                writeFileAccount(acList);
                return acList.Find(a => a.id == newAcUsername.id);
            }
            else
            {
                return new Account("", -1, newAcUsername.username, "", new List<int>(), new List<int>());
            }
        }

        [HttpPost("editAccountPassword")]
        public Account editAccountPassword([FromBody] AccountLogInfo newAcPassword)
        {
            acList = readFileAccount();
            acList.Find(a => a.id == newAcPassword.id).password = newAcPassword.password;
            writeFileAccount(acList);

            return new Account("", 1, "", newAcPassword.password, new List<int>(), new List<int>());
        }

        [HttpPost("editAccountProfilePicture")]
        public Account editAccountProfilePicture([FromBody] Account newProfilePicture)
        {
            acList = readFileAccount();
            acList.Find(a => a.id == newProfilePicture.id).pfp = newProfilePicture.pfp;
            writeFileAccount(acList);

            return new Account(newProfilePicture.pfp, 1, "", "", new List<int>(), new List<int>());
        }

        [HttpPost("leaveGroup")]
        public void leaveGroup([FromBody] GroupLeaveInfo gpLvInfo)
        {
            acList = readFileAccount();
            gpList = readFileGroups();
            psList = readFilePostIt();

            acList.Find(ac => ac.id == gpLvInfo.idUserLeaving)?.groups.Remove(gpLvInfo.idGroupToLeave);

            bool groupToDelete = true;

            foreach(Account ac in acList)
            {
                if (ac.groups.Contains(gpLvInfo.idGroupToLeave) && ac.id != 0)
                {
                    groupToDelete = false;
                }
            }

            if (groupToDelete == false)
            {
                psList.RemoveAll(ps => ps.userId == gpLvInfo.idUserLeaving && ps.group == gpLvInfo.idGroupToLeave);
                writeFilePostIt(psList);
                writeFileAccount(acList);
            }
            else
            {
                acList.Find(ac => ac.id == 0)?.groups.Remove(gpLvInfo.idGroupToLeave);

                psList.RemoveAll(ps => ps.group == gpLvInfo.idGroupToLeave);

                gpList.Remove(gpList.Find(gp => gp.id == gpLvInfo.idGroupToLeave));

                writeFilePostIt(psList);
                writeFileAccount(acList);
                writeFileGroups(gpList);
            }
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
