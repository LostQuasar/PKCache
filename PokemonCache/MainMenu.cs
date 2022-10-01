using SDL2;
using System.Runtime.InteropServices;

namespace PokemonCache
{
    public class MainMenu
    {
        public static void Render()
        {
            IntPtr logo_ptr = SDL_image.IMG_Load(PokemonCache.basePath + "png/logo.png");
            SDL.SDL_Surface logo = Marshal.PtrToStructure<SDL.SDL_Surface>(logo_ptr);
            SDL.SDL_Rect logoRect = new() { x = (PokemonCache.SCREEN_WIDTH - logo.w) / 2, y = 60 };
            PokemonCache.CheckErr(SDL.SDL_BlitSurface(logo_ptr, ref PokemonCache.clipRect, PokemonCache.screen_ptr, ref logoRect));
            SDL.SDL_FreeSurface(logo_ptr);

            IntPtr buttonSelected_ptr = SDL_image.IMG_Load(PokemonCache.basePath + "png/button_selected.png");
            SDL.SDL_Surface buttonSelected = Marshal.PtrToStructure<SDL.SDL_Surface>(buttonSelected_ptr);
            SDL.SDL_Rect buttonSelectedRect = new() { x = (PokemonCache.SCREEN_WIDTH - buttonSelected.w) / 2, y = PokemonCache.SCREEN_HEIGHT - ((buttonSelected.h / 2) + 120) };
            PokemonCache.CheckErr((SDL.SDL_BlitSurface(buttonSelected_ptr, ref PokemonCache.clipRect, PokemonCache.screen_ptr, ref buttonSelectedRect)));
            SDL.SDL_FreeSurface(buttonSelected_ptr);

            IntPtr font_small = SDL_ttf.TTF_OpenFont(PokemonCache.basePath + "font/LexendDeca-regular.ttf", 54);

            IntPtr beginText_ptr = SDL_ttf.TTF_RenderText_Blended(font_small, "Begin", new SDL.SDL_Color() { r = 0xFF, b = 0xFF, g = 0xFF });
            SDL.SDL_Surface beginText = Marshal.PtrToStructure<SDL.SDL_Surface>(beginText_ptr);
            SDL.SDL_Rect beginRect = new() { x = (PokemonCache.SCREEN_WIDTH - beginText.w) / 2, y = PokemonCache.SCREEN_HEIGHT - ((beginText.h / 2) + 120) };
            PokemonCache.CheckErr(SDL.SDL_BlitSurface(beginText_ptr, ref PokemonCache.clipRect, PokemonCache.screen_ptr, ref beginRect));
            SDL.SDL_FreeSurface(beginText_ptr);

            SDL_ttf.TTF_CloseFont(font_small);
        }
    }
}

