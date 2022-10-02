using SDL2;
using System.Runtime.InteropServices;

namespace PokemonCache
{
    internal class UI_Elements
    {
        internal static IntPtr font_small;
        internal static IntPtr font_large;
        private static SDL.SDL_Surface button;
        private static IntPtr button_ptr;
        private static IntPtr buttonSelected_ptr;
        internal static void Init()
        {
            font_small = SDL_ttf.TTF_OpenFont(PokemonCache.basePath + "font/LexendDeca-Regular.ttf", 52);
            if (font_small == IntPtr.Zero)
            {
                Console.WriteLine("Unable to load font_small");
            }
            button_ptr = SDL_image.IMG_Load(PokemonCache.basePath + "png/button.png");
            if (button_ptr == IntPtr.Zero)
            {
                Console.WriteLine("Unable to load button.png");
            }
            buttonSelected_ptr = SDL_image.IMG_Load(PokemonCache.basePath + "png/button_selected.png");
            if (buttonSelected_ptr == IntPtr.Zero)
            {
                Console.WriteLine("Unable to load button_selected.png");
            }
            button = Marshal.PtrToStructure<SDL.SDL_Surface>(button_ptr);
        }

        internal static void RenderButton(int y_offset, string text, bool selected)
        {
            IntPtr buttonTexture = selected ? buttonSelected_ptr : button_ptr; 
            SDL.SDL_Rect buttonRect = new() { x = (PokemonCache.SCREEN_WIDTH - button.w) / 2, y = y_offset - (button.h / 2) };
            PokemonCache.CheckErr((SDL.SDL_BlitSurface(buttonTexture, ref PokemonCache.clipRect, PokemonCache.screen_ptr, ref buttonRect)));

            IntPtr textSurface_ptr = SDL_ttf.TTF_RenderText_Blended(font_small, text, new SDL.SDL_Color() { r = 0xFF, b = 0xFF, g = 0xFF }); ;
            SDL.SDL_Surface textSurface = Marshal.PtrToStructure<SDL.SDL_Surface>(textSurface_ptr);
            SDL.SDL_Rect textSurfaceRect = new() { x = (PokemonCache.SCREEN_WIDTH - textSurface.w) / 2, y = y_offset - (button.h / 2) + (button.h - textSurface.h) / 2 };
            PokemonCache.CheckErr(SDL.SDL_BlitSurface(textSurface_ptr, ref PokemonCache.clipRect, PokemonCache.screen_ptr, ref textSurfaceRect));
            SDL.SDL_FreeSurface(textSurface_ptr);
        }

        internal static void RenderErrorText(string text)
        {
            IntPtr textSurface_ptr = SDL_ttf.TTF_RenderText_Blended_Wrapped(font_small, text, new SDL.SDL_Color() { r = 0xFF, b = 0xFF, g = 0xFF }, 400); ;
            SDL.SDL_Surface textSurface = Marshal.PtrToStructure<SDL.SDL_Surface>(textSurface_ptr);
            SDL.SDL_Rect textSurfaceRect = new() { x = (PokemonCache.SCREEN_WIDTH - textSurface.w) / 2, y = (PokemonCache.SCREEN_HEIGHT - textSurface.h) / 2 - 60};
            PokemonCache.CheckErr(SDL.SDL_BlitSurface(textSurface_ptr, ref PokemonCache.clipRect, PokemonCache.screen_ptr, ref textSurfaceRect));
            SDL.SDL_FreeSurface(textSurface_ptr);
        }

        internal static void Close()
        {
            SDL.SDL_FreeSurface(button_ptr);
            SDL.SDL_FreeSurface(buttonSelected_ptr);
            SDL_ttf.TTF_CloseFont(font_small);
        }
    }
}
  