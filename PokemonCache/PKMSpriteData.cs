using PokeSpriteJSON;
using System.Text.Json;

namespace PokemonCache
{
internal class PKMSpriteData
    {
        public static PokeData? pokeData;

        public static void Init()
        {
            string jsonString = File.ReadAllText(PokemonCache.basePath + "json/pokemon.json");
            pokeData = JsonSerializer.Deserialize<PokeData>(jsonString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public static string GetSlugFromSpecies(uint speciesId)
        {
            throw new NotImplementedException();
        }

    }
}
