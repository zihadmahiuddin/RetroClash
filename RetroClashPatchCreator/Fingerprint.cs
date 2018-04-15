using System.Collections.Generic;
using Newtonsoft.Json;
using RetroClashPatchCreator.Models;

namespace RetroClashPatchCreator
{
    public class Fingerprint
    {
        [JsonProperty("files")]
        public List<File> Files = new List<File>();

        [JsonProperty("sha")]
        public string Sha { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
