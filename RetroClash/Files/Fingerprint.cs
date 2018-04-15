using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace RetroClash.Files
{
    public class Fingerprint : IDisposable
    {
        public Fingerprint()
        {
            Version = new string[3];

            try
            {
                if (File.Exists("Assets/fingerprint.json"))
                {
                    Json = File.ReadAllText("Assets/fingerprint.json");
                    var json = JObject.Parse(Json);
                    Sha = json["sha"].ToObject<string>();
                    Version = json["version"].ToObject<string>().Split('.');
                }
                else
                {
                    Console.WriteLine("The Fingerprint cannot be loaded, the file does not exist.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load the Fingerprint.");
            }
        }

        public string Json { get; set; }

        public string Sha { get; set; }

        public string[] Version { get; set; }

        public void Dispose()
        {
            Json = null;
            Sha = null;
            Version = null;
        }
    }
}
