using PKHeX.Core;
using PKSpriteJSON;
using SDL2;
using System.Runtime.InteropServices;

namespace PKCache.Screen
{
    internal class UI_Elements
    {
        private static IntPtr font_small;
        private static IntPtr font_large;
        private static IntPtr font_code;
        private static SDL.SDL_Surface button;
        private static IntPtr button_ptr;
        private static SDL.SDL_Surface scrollUp;
        private static IntPtr scrollUp_ptr;
        private static SDL.SDL_Surface scrollDown;
        private static IntPtr scrollDown_ptr;
        private static IntPtr buttonSelected_ptr;
        internal static readonly int ScrollMod = 2;

        public static void Init()
        {
            font_small = SDL_ttf.TTF_OpenFont(Program.basePath + "font/LexendDeca-Regular.ttf", 48);
            if (font_small == IntPtr.Zero)
            {
                Console.WriteLine("Unable to load font_small");
            }
            font_large = SDL_ttf.TTF_OpenFont(Program.basePath + "font/LexendDeca-Bold.ttf", 64);
            if (font_small == IntPtr.Zero)
            {
                Console.WriteLine("Unable to load font_large");
            }
            font_code = SDL_ttf.TTF_OpenFont(Program.basePath + "font/RobotoMono-Medium.ttf", 64);
            if (font_code == IntPtr.Zero)
            {
                Console.WriteLine("Unable to load font_code");
            }
            button_ptr = SDL_image.IMG_Load(Program.basePath + "png/button.png");
            if (button_ptr == IntPtr.Zero)
            {
                Console.WriteLine("Unable to load button.png");
            }
            buttonSelected_ptr = SDL_image.IMG_Load(Program.basePath + "png/button_selected.png");
            if (buttonSelected_ptr == IntPtr.Zero)
            {
                Console.WriteLine("Unable to load button_selected.png");
            }
            scrollUp_ptr = SDL_image.IMG_Load(Program.basePath + "png/scroll_up.png");
            if (buttonSelected_ptr == IntPtr.Zero)
            {
                Console.WriteLine("Unable to load scroll_up.png");
            }
            scrollDown_ptr = SDL_image.IMG_Load(Program.basePath + "png/scroll_down.png");
            if (buttonSelected_ptr == IntPtr.Zero)
            {
                Console.WriteLine("Unable to load scroll_down.png");
            }

            button = Marshal.PtrToStructure<SDL.SDL_Surface>(button_ptr);
            scrollUp = Marshal.PtrToStructure<SDL.SDL_Surface>(scrollUp_ptr);
            scrollDown = Marshal.PtrToStructure<SDL.SDL_Surface>(scrollDown_ptr);
        }

        internal static void RenderButton(int y_offset, string text, bool selected, int x_offset = 0)
        {
            IntPtr buttonTexture = selected ? buttonSelected_ptr : button_ptr;
            SDL.SDL_Rect buttonRect = new() { x = (ScreenHandler.SCREEN_WIDTH - button.w) / 2 + x_offset, y = y_offset - button.h / 2 };
            ScreenHandler.CheckErr(SDL.SDL_BlitSurface(buttonTexture, IntPtr.Zero, ScreenHandler.screen_ptr, ref buttonRect));

            IntPtr textSurface_ptr = SDL_ttf.TTF_RenderText_Blended(font_small, text, new SDL.SDL_Color() { r = 0xFF, b = 0xFF, g = 0xFF }); ;
            SDL.SDL_Surface textSurface = Marshal.PtrToStructure<SDL.SDL_Surface>(textSurface_ptr);
            SDL.SDL_Rect textSurfaceRect = new() { x = (ScreenHandler.SCREEN_WIDTH - textSurface.w) / 2 + x_offset, y = y_offset - textSurface.h / 2 };
            ScreenHandler.CheckErr(SDL.SDL_BlitSurface(textSurface_ptr, IntPtr.Zero, ScreenHandler.screen_ptr, ref textSurfaceRect));
            SDL.SDL_FreeSurface(textSurface_ptr);
        }

        internal static void RenderErrorText(string text)
        {
            IntPtr textSurface_ptr = SDL_ttf.TTF_RenderText_Blended_Wrapped(font_small, "ERROR:\n" + text, new SDL.SDL_Color() { r = 0x41, b = 0x41, g = 0x41 }, 400); ;
            SDL.SDL_Surface textSurface = Marshal.PtrToStructure<SDL.SDL_Surface>(textSurface_ptr);
            SDL.SDL_Rect textSurfaceRect = new() { x = (ScreenHandler.SCREEN_WIDTH - textSurface.w) / 2, y = (ScreenHandler.SCREEN_HEIGHT - textSurface.h) / 2 - 60 };
            ScreenHandler.CheckErr(SDL.SDL_BlitSurface(textSurface_ptr, IntPtr.Zero, ScreenHandler.screen_ptr, ref textSurfaceRect));
            SDL.SDL_FreeSurface(textSurface_ptr);
        }

        internal static void RenderLargeText(string text)
        {
            IntPtr textSurface_ptr = SDL_ttf.TTF_RenderText_Blended_Wrapped(font_large, text, new SDL.SDL_Color() { r = 0x41, b = 0x41, g = 0x41 }, 400); ;
            SDL.SDL_Surface textSurface = Marshal.PtrToStructure<SDL.SDL_Surface>(textSurface_ptr);
            SDL.SDL_Rect textSurfaceRect = new() { x = (ScreenHandler.SCREEN_WIDTH - textSurface.w) / 2, y = (ScreenHandler.SCREEN_HEIGHT - textSurface.h) / 2 - 60 };
            ScreenHandler.CheckErr(SDL.SDL_BlitSurface(textSurface_ptr, IntPtr.Zero, ScreenHandler.screen_ptr, ref textSurfaceRect));
            SDL.SDL_FreeSurface(textSurface_ptr);
        }

        internal static void RenderPKMButton(int y_offset, string nicname, bool selected, PKM pkm)
        {
            IntPtr buttonTexture = selected ? buttonSelected_ptr : button_ptr;
            SDL.SDL_Rect buttonRect = new() { x = (ScreenHandler.SCREEN_WIDTH - button.w) / 2, y = y_offset - button.h / 2 };
            ScreenHandler.CheckErr(SDL.SDL_BlitSurface(buttonTexture, IntPtr.Zero, ScreenHandler.screen_ptr, ref buttonRect));

            IntPtr pkmPng_ptr = SDL_image.IMG_Load(PKM_IconPath(pkm));
            if (pkmPng_ptr != IntPtr.Zero)
            {
                SDL.SDL_Surface pkmPng = Marshal.PtrToStructure<SDL.SDL_Surface>(pkmPng_ptr);
                SDL.SDL_Rect pkmPngRect = new() { x = buttonRect.x - 20, y = y_offset - button.h / 2 - 60, w = pkmPng.w * 24, h = pkmPng.h * 24 };
                ScreenHandler.CheckErr(SDL.SDL_BlitScaled(pkmPng_ptr, ref ScreenHandler.clipRect, ScreenHandler.screen_ptr, ref pkmPngRect));
                SDL.SDL_FreeSurface(pkmPng_ptr);
            }

            IntPtr textSurface_ptr = SDL_ttf.TTF_RenderText_Blended(font_small, nicname, new SDL.SDL_Color() { r = 0xFF, b = 0xFF, g = 0xFF }); ;
            SDL.SDL_Surface textSurface = Marshal.PtrToStructure<SDL.SDL_Surface>(textSurface_ptr);
            SDL.SDL_Rect textSurfaceRect = new() { x = buttonRect.x + 140, y = y_offset - button.h / 2 + (button.h - textSurface.h) / 2 };
            ScreenHandler.CheckErr(SDL.SDL_BlitSurface(textSurface_ptr, IntPtr.Zero, ScreenHandler.screen_ptr, ref textSurfaceRect));
            SDL.SDL_FreeSurface(textSurface_ptr);
        }

        private static string PKM_IconPath(PKM pkm)
        {
            int speciesId = pkm.Species;
            bool isShiny = pkm.IsShiny;
            bool isFemale = pkm.Gender == 1;
            int form = pkm.Form;
            bool isEgg = pkm.IsEgg;

            Id pkmSpriteData = PKMSpriteData.pokeData!.Id[$"{speciesId:000}"];
            string slug = pkmSpriteData.Slug;

            string pkmFolder = Program.basePath + "png/pokemon/" + (isShiny ? "shiny/" : "regular/");
            pkmFolder += pkmSpriteData.HasFemale & isFemale ? "female/" : "";

            string formString = "";
            if (form != 0)
            {
                formString = "/" + pkmSpriteData.Forms[form];
                pkmFolder += "form/";
            }
            
            if (isEgg)
            {
                return Program.basePath + "png/pokemon/egg.png";
            }

            return pkmFolder + slug + formString + ".png";
        }

        public static void Quit()
        {
            SDL.SDL_FreeSurface(button_ptr);
            SDL.SDL_FreeSurface(buttonSelected_ptr);
            SDL_ttf.TTF_CloseFont(font_small);
            SDL_ttf.TTF_CloseFont(font_code);
            SDL_ttf.TTF_CloseFont(font_large);
        }

        internal static void RenderSelPKM(PKM? pkm)
        {
            if (pkm == null)
            {
                return;
            }

            IntPtr pkmPng_ptr = SDL_image.IMG_Load(PKM_IconPath(pkm));
            if (pkmPng_ptr != IntPtr.Zero)
            {
                SDL.SDL_Surface pkmPng = Marshal.PtrToStructure<SDL.SDL_Surface>(pkmPng_ptr);
                SDL.SDL_Rect pkmPngRect = new() { x = -20, y = -70, w = pkmPng.w * 24, h = pkmPng.h * 24 };
                ScreenHandler.CheckErr(SDL.SDL_BlitScaled(pkmPng_ptr, ref ScreenHandler.clipRect, ScreenHandler.screen_ptr, ref pkmPngRect));
                SDL.SDL_FreeSurface(pkmPng_ptr);
            }
        }

        internal static void DrawCodeEnter(int selIndex, string text)
        {
            IntPtr textSurface_ptr = SDL_ttf.TTF_RenderText_Blended_Wrapped(font_code, text, new SDL.SDL_Color() { r = 0x41, b = 0x41, g = 0x41 }, 400); ;
            SDL.SDL_Surface textSurface = Marshal.PtrToStructure<SDL.SDL_Surface>(textSurface_ptr);
            SDL.SDL_Rect textSurfaceRect = new() { x = (ScreenHandler.SCREEN_WIDTH - textSurface.w) / 2, y = (ScreenHandler.SCREEN_HEIGHT - textSurface.h) / 2 - 60 };

            ScreenHandler.CheckErr(SDL.SDL_BlitSurface(textSurface_ptr, IntPtr.Zero, ScreenHandler.screen_ptr, ref textSurfaceRect));
            SDL.SDL_FreeSurface(textSurface_ptr);
            if (text[selIndex] != 'F')
            {
                SDL.SDL_Rect scrollUpRect = new() { x = textSurfaceRect.x + (textSurface.w / 6) * selIndex + 5, y = textSurfaceRect.y - 20 };
                ScreenHandler.CheckErr(SDL.SDL_BlitSurface(scrollUp_ptr, IntPtr.Zero, ScreenHandler.screen_ptr, ref scrollUpRect));

            }
            if (text[selIndex] != '0')
            {
                SDL.SDL_Rect scrollDownRect = new() { x = textSurfaceRect.x + (textSurface.w / 6) * selIndex + 5, y = textSurfaceRect.y + textSurface.h - scrollDown.h + 20 };
                ScreenHandler.CheckErr(SDL.SDL_BlitSurface(scrollDown_ptr, IntPtr.Zero, ScreenHandler.screen_ptr, ref scrollDownRect));

            }
        }
    }
}
