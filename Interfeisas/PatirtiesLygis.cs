using AdmiralCapital.Balansas;
using DSharpPlus.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmiralCapital.Interfeisas
{
    public abstract class ExperienceLevel
    {
        public string UserName { get; set; }
        public int XP { get; set; }

        public abstract string GetLevel();
    }

    public class Naujokas : ExperienceLevel
    {
        public override string GetLevel()
        {
            return "Pradžiamokslis";
        }
    }

    public class Pazenges : ExperienceLevel
    {
        public override string GetLevel()
        {
            return "Pažengęs";
        }
    }

    public class Ekspertas : ExperienceLevel
    {
        public override string GetLevel()
        {
            return "Priklausomas";
        }
    }

    public class Veteranas : ExperienceLevel
    {
        public override string GetLevel()
        {
            return "Veteranas";
        }
    }
    public class ExperienceService
    {
        public ExperienceLevel GetXp(string name)
        {
            string username = name;


            var path = @"C:\Users\Nedas\Desktop\AdmiralCapital\bin\Debug\UserInfo.json";


            var json = File.ReadAllText(path);
            var jsonObj = JObject.Parse(json);
            var members = jsonObj["members"].ToObject<List<DUser>>();

            var userDetails = members.FirstOrDefault(u => u.UserName == username );



            var levels = new Dictionary<Func<int, bool>, Func<DUser, ExperienceLevel>>
    {
        { xp => xp < 100000, member => new Naujokas { UserName = member.UserName, XP = member.XP } },
        { xp => xp >= 100000 && xp < 1000000, member => new Pazenges {UserName = member.UserName, XP = member.XP} },
        { xp => xp >= 1000000 && xp < 10000000, member => new Ekspertas {UserName = member.UserName, XP = member.XP} },
        { xp => xp >= 10000000, member => new Veteranas { UserName = member.UserName, XP = member.XP } },
    };

            foreach (var level in levels)
            {
                if (level.Key(userDetails.XP))
                    return level.Value(userDetails);
            }

            throw new InvalidOperationException("XP range is not defined for this user.");
        }


    }
}
