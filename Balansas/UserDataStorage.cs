using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmiralCapital.Balansas
{
    public class UserDataStorage : IUserStorage
    {
        private readonly string _filePath = @"C:\Users\Nedas\Desktop\AdmiralCapital\bin\Debug\UserInfo.json";

        public bool StoreUserDetails(DUser user)
        {
            try
            {
                var json = File.ReadAllText(_filePath);
                var jsonObj = JObject.Parse(json);
                var members = jsonObj["members"].ToObject<List<DUser>>();
                members.Add(user);
                jsonObj["members"] = JArray.FromObject(members);
                File.WriteAllText(_filePath, jsonObj.ToString());

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool CheckUserExists(string username, ulong guildID)
        {
            try
            {
                var json = File.ReadAllText(_filePath);
                var jsonObj = JObject.Parse(json);
                var members = jsonObj["members"].ToObject<List<DUser>>();

                return members.Any(user => user.UserName == username && user.GuildID == guildID);
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }

        public DUser GetUser(string username, ulong guildID)
        {
            try
            {
                var json = File.ReadAllText(_filePath);
                var jsonObj = JObject.Parse(json);
                var members = jsonObj["members"].ToObject<List<DUser>>();

                return members.FirstOrDefault(user => user.UserName == username && user.GuildID == guildID);
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }

        public bool UpdateBalance(string username, ulong guildID, int amount)
        {
            try
            {
                var json = File.ReadAllText(_filePath);
                var jsonObj = JObject.Parse(json);
                var members = jsonObj["members"].ToObject<List<DUser>>();

                var user = members.FirstOrDefault(u => u.UserName == username && u.GuildID == guildID);
                if (user != null && user.Balance <= 5)
                {
                    user.Balance += amount;
                    jsonObj["members"] = JArray.FromObject(members);
                    File.WriteAllText(_filePath, jsonObj.ToString());
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }
    }
}
