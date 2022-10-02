using SDL2;
using System.Runtime.InteropServices;

namespace PokemonCache
{
    internal class MainMenu
    {
        internal static void Render()
        {
            IntPtr logo_ptr = SDL_image.IMG_Load(PokemonCache.basePath + "png/logo.png");
            SDL.SDL_Surface logo = Marshal.PtrToStructure<SDL.SDL_Surface>(logo_ptr);
            SDL.SDL_Rect logoRect = new() { x = (PokemonCache.SCREEN_WIDTH - logo.w) / 2, y = 60 };
            PokemonCache.CheckErr(SDL.SDL_BlitSurface(logo_ptr, ref PokemonCache.clipRect, PokemonCache.screen_ptr, ref logoRect));
            SDL.SDL_FreeSurface(logo_ptr);
            UI_Elements.RenderButton(380, "Begin", true);
        }
    }
}

