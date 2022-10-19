namespace PKCache.Screen
{
    internal class LocationScreen
    {
        public static int selectionIndex = 0;
        public readonly static int maxSelectionIndex = 1;
        public static void Render()
        {
            UI_Elements.RenderButton(170, "Save to game", selectionIndex == 0);
            UI_Elements.RenderButton(310, "Save to cache", selectionIndex == 1);
            UI_Elements.RenderSelPKM(SaveHandler.selectedPKM);
        }
    }
}
