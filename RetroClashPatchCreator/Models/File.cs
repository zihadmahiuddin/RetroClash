using Newtonsoft.Json;

namespace RetroClashPatchCreator.Models
{
    public class File
    {
        [JsonProperty("sha")]
        public string Sha { get; set; }

        [JsonProperty("file")]
        public string FilePath { get; set; }
    }
}
