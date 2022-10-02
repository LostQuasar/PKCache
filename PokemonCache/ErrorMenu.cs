namespace PokemonCache
{
    internal class ErrorMenu
    {
        internal static void Render(string error)
        {
            UI_Elements.RenderErrorText(error);
            UI_Elements.RenderButton(360, "Return to Menu", true);
        }
    }
}
