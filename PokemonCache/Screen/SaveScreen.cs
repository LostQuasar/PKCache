namespace PKCache.Screen
{
    internal class SaveScreen
    {
        public static int selectionIndex = 0;
        public static int maxSelectionIndex;
        internal static void Render(string[] savPaths)
        {
            maxSelectionIndex = Program.savList.Length - 1;
            if (maxSelectionIndex == -1)
            {
                Program.errorOccured = Tuple.Create(true, "No save files present!");
            }
            else
            {
                string[] viewPaths = new string[3];
                int offset = 0;
                if (maxSelectionIndex > 2)
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
                    string pathShort = savPaths[offset - 1].Replace(Program.savPath, null).Replace(".sav", null);
                    pathShort = pathShort.Length <= 17 ? pathShort : pathShort[..15] + "...";

                    UI_Elements.RenderButton(0, pathShort, false);
                }
                foreach (string path in viewPaths)
                {
                    string pathShort = path.Replace(Program.savPath, null).Replace(".sav", null);
                    pathShort = pathShort.Length <= 17 ? pathShort : pathShort[..15] + "...";

                    int index = Array.IndexOf(savPaths, path);
                    UI_Elements.RenderButton(120 + 120 * (index - offset), pathShort, selectionIndex == index);
                }
                if (offset + 2 < maxSelectionIndex)
                {
                    string pathShort = savPaths[offset + 3].Replace(Program.savPath, null).Replace(".sav", null);
                    pathShort = pathShort.Length <= 17 ? pathShort : pathShort[..15] + "...";

                    UI_Elements.RenderButton(480, pathShort, false);
                }
            }
        }
    }
}

