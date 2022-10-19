using System.Transactions;

namespace PKCache.Screen
{
    internal class GiftScreen
    {
        internal static int selectionIndex;
        internal static int maxSelectionIndex = 5;
        internal static int[] value = { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
        public static void Render()
        {
            string val = "";
            
            foreach (int i in value)
            {
                val += i.ToString("X");
            }

            UI_Elements.DrawCodeEnter(selectionIndex, val);

            UI_Elements.RenderButton(380, "Submit", true);
        }
    }
}
