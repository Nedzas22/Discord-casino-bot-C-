using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmiralCapital.config
{
    internal class JSONreader
    {
        public string Token {  get; set; }
        public string Prefix { get; set; }

        public async Task ReadJSON()
        {
            using (StreamReader sr = new StreamReader("config.json"))
            {
                string json = await sr.ReadToEndAsync();
                JSONStructure data = JsonConvert.DeserializeObject<JSONStructure>(json);
                Token = data.Token;
                Prefix = data.Prefix;
            }
        }
    }
    internal sealed class JSONStructure
    {
        public string Token { get; set; }
        public string Prefix { get; set; }
    }
}
