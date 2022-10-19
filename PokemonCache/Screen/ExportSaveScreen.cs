namespace PKCache.Screen
{
    internal class ExportSaveScreen
    {
        public static int selectionIndex = 0;
        public static int maxSelectionIndex;
        internal static void Render()
        {
            string[] savPaths = SaveHandler.saveDict.Keys.ToArray();
            maxSelectionIndex =  savPaths.Length - 1;
            if (maxSelectionIndex == -1)
            {
                Program.errorOccured = Tuple.Create(true, "No save files present!");
            }
            else
            {
                int offset = 1;

                if (savPaths.Length > 3)
                {
                    offset = (selectionIndex - (selectionIndex % UI_Elements.ScrollMod + 1)) * -1;
                }

                foreach (int visualIndex in Enumerable.Range(0, 5))
                {
                    int indexOffset = visualIndex - offset;
                    if (indexOffset >= 0 & indexOffset < savPaths.Length)
                    {
                        string shortString = savPaths[indexOffset].Replace(Program.savPath, null).Replace(".sav", null);
                        shortString = shortString.Length <= 17 ? shortString : shortString[..16] + "...";
                        if (indexOffset == SaveScreen.selectionIndex)
                        {
                            shortString = "Original Save";
                        }
                        
                        UI_Elements.RenderButton(120 * visualIndex, shortString, selectionIndex == indexOffset, 30);
                    }
                }
            }
            UI_Elements.RenderSelPKM(SaveHandler.selectedPKM);
        }
    }
}

