using PKHeX.Core;

namespace PokemonCache
{
    internal class SaveHandler
    {
        private static SaveFile? savFile;

        public static void LoadSave(string savPath)
        {
            byte[] savBytes = File.ReadAllBytes(savPath);
            savFile = SaveUtil.GetVariantSAV(savBytes);
            if (savFile == null)
            {
                PokemonCache.errorOccured = Tuple.Create(true, "Could not load save");
                return;
            }
            PKM[] locPkmList = savFile.GetAllPKM().ToArray();
            if (locPkmList == null)
            {
                PokemonCache.errorOccured = Tuple.Create(true, "PKM could not be loaded");
            }
            else if (locPkmList.Length == 0)
            {
                PokemonCache.errorOccured = Tuple.Create(true, "No pokemon found");
            }
            else
            {
                PKMMenu.pKMs = locPkmList;
            }
        }
    }
}
