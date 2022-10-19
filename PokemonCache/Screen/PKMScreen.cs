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
                int offset = 1;

                if (pkms.Length > 3)
                {
                    offset = (selectionIndex - (selectionIndex % UI_Elements.ScrollMod + 1)) * -1;
                }

                foreach (int visualIndex in Enumerable.Range(0, 5))
                {
                    int indexOffset = visualIndex - offset;
                    if (indexOffset >= 0 & indexOffset < pkms.Length)
                    {
                        PKM pkm = pkms[indexOffset];
                        UI_Elements.RenderPKMButton(120 * visualIndex, Clean(pkm.Nickname), selectionIndex == indexOffset, pkm);
                    }
                }
            }
        }

        public static string Clean(string s)
        {
            StringBuilder sb = new(s);

            sb.Replace("’", "'");
            sb.Replace("♀", " F");
            sb.Replace("♂", " M");
            sb.Replace("タマゴ", "Egg");

            return sb.ToString();
        }
    }
}