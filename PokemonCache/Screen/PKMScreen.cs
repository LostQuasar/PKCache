using PKHeX.Core;
using System.Text;

namespace PKCache.Screen
{
    internal class PKMScreen
    {
        public static int selectionIndex = 0;
        public static int maxSelectionIndex;
        public static PKM[] pkms = Array.Empty<PKM>();
        internal static void Render()
        {
            maxSelectionIndex = pkms.Length - 1;
            if (maxSelectionIndex == -1)
            {
                Program.errorOccured = Tuple.Create(true, "No PKM present!");
            }

            else
            {
                PKM[] viewPkm = new PKM[3];
                int offset = 0;
                if (maxSelectionIndex > 2)
                {
                    offset = Math.Clamp(selectionIndex, 0, maxSelectionIndex - 2);
                    Array.Copy(pkms, offset, viewPkm, 0, 3);
                }
                else
                {
                    viewPkm = pkms;
                }

                if (offset > 0)
                {
                    PKM pkm = pkms[offset - 1];

                    UI_Elements.RenderPKMButton(0, Clean(pkm.Nickname), false, pkm.Species, pkm.IsShiny, pkm.Gender == 1, pkm.Form);
                }
                foreach (PKM pkm in viewPkm)
                {
                    int index = Array.IndexOf(pkms, pkm);
                    UI_Elements.RenderPKMButton(120 + 120 * (index - offset), Clean(pkm.Nickname), selectionIndex == index, pkm.Species, pkm.IsShiny, pkm.Gender == 1, pkm.Form);
                }
                if (offset + 2 < maxSelectionIndex)
                {
                    PKM pkm = pkms[offset + 3];

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