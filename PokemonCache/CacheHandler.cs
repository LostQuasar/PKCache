using PKHeX.Core;

namespace PKCache
{
    internal class CacheHandler
    {
        public static PKM[] pkmList = Array.Empty<PKM>();
        public static void Init()
        {
            pkmList = Array.Empty<PKM>();
            string[] files = Directory.GetFiles(Program.basePath + "cache/");
            foreach (string file in files)
            {
                FileInfo fi = new(file);
                FileUtil.TryGetPKM(File.ReadAllBytes(file), out PKM? pkm, fi.Extension);
                if (pkm != null)
                {
                    pkmList = pkmList.Append(pkm).ToArray();
                }
            }
        }
    }
}
