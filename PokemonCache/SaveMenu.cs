using SDL2;

namespace PokemonCache
{
    internal class SaveMenu
    {
        public static int selectionIndex = 0;
        public static int maxSelectionIndex;
        internal static void Render(string[] savPaths)
        {

            if (maxSelectionIndex == -1)
            {
                PokemonCache.errorOccured = Tuple.Create(true, "ERROR\nNo save files present!");
            }
            else
            {
                string[] viewPaths = new string[3];
                int offset = 0;
                if (maxSelectionIndex > 3)
                {
                    offset = Math.Clamp(selectionIndex, 0, maxSelectionIndex - 2);
                    Array.Copy(savPaths, offset, viewPaths, 0, 3);
                }
                else
                {
                    viewPaths = savPaths;
                }

                if (offset > 0)
                {
                    UI_Elements.RenderButton(0, savPaths[offset - 1], false);
                }
                foreach (string path in viewPaths)
                {
                    string pathShort = path.Replace(PokemonCache.basePath, null).Replace(".sav", null);
                    pathShort = pathShort.Length <= 17 ? pathShort : pathShort[..15] + "...";

                    int index = Array.IndexOf(savPaths, path);
                    UI_Elements.RenderButton(120 + 120 * (index - offset), pathShort, selectionIndex == index);
                }
                if (offset + 2 < maxSelectionIndex)
                {
                    UI_Elements.RenderButton(480, savPaths[offset + 3], false);
                }
            }
        }
    }
}

