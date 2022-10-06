using PKSpriteJSON;
using System.Text.Json;

namespace PKCache
{
internal class PKMSpriteData
    {
        public static PokeData? pokeData;

        public static void Init()
        {
            string jsonString = File.ReadAllText(Program.basePath + "json/pokemon.json");
            pokeData = JsonSerializer.Deserialize<PokeData>(jsonString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
