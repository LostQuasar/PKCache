using PKCache;
using SDL2;
using System.Runtime.InteropServices;

namespace PKCache.Screen
{
    internal class MainScreen
    {
        internal static void Render()
        {
            IntPtr logo_ptr = SDL_image.IMG_Load(Program.basePath + "png/logo.png");
            SDL.SDL_Surface logo = Marshal.PtrToStructure<SDL.SDL_Surface>(logo_ptr);
            SDL.SDL_Rect logoRect = new() { x = (ScreenHandler.SCREEN_WIDTH - logo.w) / 2, y = 60 };
            ScreenHandler.CheckErr(SDL.SDL_BlitSurface(logo_ptr, ref ScreenHandler.clipRect, ScreenHandler.screen_ptr, ref logoRect));
            SDL.SDL_FreeSurface(logo_ptr);
            UI_Elements.RenderButton(380, "Begin", true);
        }
    }
}

