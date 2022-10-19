using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKCache.Screen
{
    internal class ExtrasScreen
    {
        internal static int selectionIndex;
        internal static int maxSelectionIndex = 2;

        public static void Render()
        {
            UI_Elements.RenderButton(100, "Event Items", selectionIndex == 0 );
            UI_Elements.RenderButton(240, "Trade Evolve", selectionIndex == 1 );
            UI_Elements.RenderButton(380, "Mystery Gift", selectionIndex == 2 );
        }
    }
}
