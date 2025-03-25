using AdmiralCapital.Balansas;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmiralCapital.Interfeisas
{
    internal class Laimejimas
    {
        public bool Win(string username, ulong guildID, int bet, int x)
        {
            try
            {
                var path = @"C:\Users\Nedas\Desktop\AdmiralCapital\bin\Debug\UserInfo.json";
                var json = File.ReadAllText(path);
                var jsonObj = JObject.Parse(json);
                var members = jsonObj["members"].ToObject<List<DUser>>();
                foreach (var user in members)
                {
                    if (user.UserName == username && user.GuildID == guildID)
                    {


                        user.Balance = user.Balance + bet * x;
                        user.XP += bet * 10 * user.Booster;
                        jsonObj["members"] = JArray.FromObject(members);
                        File.WriteAllText(path, jsonObj.ToString());
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }


                return false;
            }
            catch (Exception ex) { return false; }
        }
    }
}
