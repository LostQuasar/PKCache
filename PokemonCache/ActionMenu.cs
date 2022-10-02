namespace PokemonCache
{
    internal class ActionMenu
    {
        public static int selectionIndex = 0;
        public readonly static int maxSelectionIndex = 2;
        internal static void Render()
        {
            UI_Elements.RenderButton(100, "Open Saves", selectionIndex == 0);
            UI_Elements.RenderButton(240, "Open Cache", selectionIndex == 1);
            UI_Elements.RenderButton(380, "Extras", selectionIndex == 2);
        }
    }
}
