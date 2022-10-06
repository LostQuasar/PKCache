namespace PKCache.Screen
{
    internal class ErrorScreen
    {
        internal static void Render(string error)
        {
            UI_Elements.RenderErrorText(error);
            UI_Elements.RenderButton(360, "Return to Menu", true);
        }
    }
}
