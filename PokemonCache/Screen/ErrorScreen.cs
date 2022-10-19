namespace PKCache.Screen
{
    internal class ErrorScreen
    {
        internal static void Render()
        {
            UI_Elements.RenderErrorText(Program.errorOccured.Item2);
            UI_Elements.RenderButton(360, "Return to Menu", true);
        }
    }
}
