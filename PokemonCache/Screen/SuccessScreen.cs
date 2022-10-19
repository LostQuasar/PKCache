namespace PKCache.Screen
{
    internal class SuccessScreen
    {
        internal static void Render()
        {
            UI_Elements.RenderLargeText("Success");
            UI_Elements.RenderButton(380, "Return to menu", true);
        }
    }
}