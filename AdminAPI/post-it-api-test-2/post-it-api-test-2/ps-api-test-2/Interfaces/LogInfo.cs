namespace ps_api_test_2.Controllers
{
    public class LogInfo
    {
        public string password { get; set; }
        public string username { get; set; }
        public LogInfo(string password, string username)
        {
            this.password = password;
            this.username = username;
        }
    }
}
