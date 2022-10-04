using PKHeX.Core;
using System;
using System.Text;

namespace PokemonCache
{
    internal class PKMMenu
    {
        public static int selectionIndex = 0;
        public static int maxSelectionIndex;
        public static PKM[] pKMs = Array.Empty<PKM>();
        internal static void Render()
        {
            maxSelectionIndex = pKMs.Length - 1;
            if ( maxSelectionIndex == -1 )
            {
                PokemonCache.errorOccured = Tuple.Create(true, "No PKM present!");
            }

            else
            {
                PKM[] viewPkm = new PKM[3];
                int offset = 0;
                if ( maxSelectionIndex > 2 )
                {
                    offset = Math.Clamp(selectionIndex, 0, maxSelectionIndex - 2);
                    Array.Copy(pKMs, offset, viewPkm, 0, 3);
                }
                else
                {
                    viewPkm = pKMs;
                }

                if (offset > 0)
                {
                    PKM pkm = pKMs[offset-1];

                    UI_Elements.RenderPKMButton(0, Clean(pkm.Nickname), false, pkm.Species, pkm.IsShiny, pkm.Gender == 1, pkm.Form);
                }
                foreach (PKM pkm in viewPkm)
                {
                    int index = Array.IndexOf(pKMs, pkm);
                    UI_Elements.RenderPKMButton(120 + 120 * (index - offset), Clean(pkm.Nickname), selectionIndex == index, pkm.Species, pkm.IsShiny, pkm.Gender == 1, pkm.Form);
                }
                if (offset + 2 < maxSelectionIndex)
                {
                    PKM pkm = pKMs[offset + 3];
                    
                    UI_Elements.RenderPKMButton(480, Clean(pkm.Nickname), false, pkm.Species, pkm.IsShiny, pkm.Gender == 1, pkm.Form);
                }
            }
        }
        public static string Clean(string s)
        {
            StringBuilder sb = new(s);

            sb.Replace("’", "'");
            sb.Replace("♀", " F");
            sb.Replace("♂", " M");

            return sb.ToString();
        }
    }
}