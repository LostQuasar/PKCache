namespace PokeSpriteJSON
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public partial class PokeData
    {
        [JsonProperty("Id")]
        public Dictionary<string, Id> Id { get; set; }
    }

    public partial class Id
    {
        [JsonProperty("Name")]
        public Name Name { get; set; }

        [JsonProperty("Slug")]
        public string Slug { get; set; }

        [JsonProperty("HasFemale")]
        public bool HasFemale { get; set; }

        [JsonProperty("forms", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Forms { get; set; }
    }

    public partial class Name
    {
        [JsonProperty("Eng")]
        public string Eng { get; set; }

        [JsonProperty("Chs")]
        public string Chs { get; set; }

        [JsonProperty("Jpn")]
        public string Jpn { get; set; }

        [JsonProperty("JpnRo")]
        public string JpnRo { get; set; }
    }
}

