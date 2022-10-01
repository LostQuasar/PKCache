using SDL2;
using System.Runtime.InteropServices;

namespace PokemonCache
{
    class PokemonCache
    {
        private static IntPtr _Window = IntPtr.Zero;
        public static IntPtr Renderer;
        public static IntPtr screen_ptr;
        private static IntPtr texture;
        public static SDL.SDL_Rect clipRect;
        private static SDL.SDL_Surface screen;
        public static readonly int SCREEN_WIDTH = 640;
        public static readonly int SCREEN_HEIGHT = 480;
        public static readonly int BPP = 32;
        public static string basePath = "";

        enum ScreenState
        {
            mainMenu
        }

        static void Main()
        {
            ScreenState screenCurrent = ScreenState.mainMenu;
            bool quit = false;
            basePath = Directory.GetCurrentDirectory() + "/res/";
            CheckErr(SDL.SDL_Init(SDL.SDL_INIT_VIDEO));
            CheckErr(SDL_ttf.TTF_Init());
            _Window = SDL.SDL_CreateWindow("main", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, SCREEN_WIDTH, SCREEN_HEIGHT, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
            Renderer = SDL.SDL_CreateRenderer(_Window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            screen_ptr = SDL.SDL_CreateRGBSurface(0, SCREEN_WIDTH, SCREEN_HEIGHT, BPP, 0, 0, 0, 0);
            screen = Marshal.PtrToStructure<SDL.SDL_Surface>(screen_ptr);
            texture = SDL.SDL_CreateTexture(Renderer, SDL.SDL_PIXELFORMAT_ARGB8888, (int)SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING, SCREEN_WIDTH, SCREEN_HEIGHT);

            SDL.SDL_GetClipRect(screen_ptr, out clipRect);
            CheckErr(SDL.SDL_FillRect(screen_ptr, ref clipRect, SDL.SDL_MapRGB(screen.format, 0x10, 0xF0, 0xA0))); //Draw BG

            switch (screenCurrent)
            {
                case ScreenState.mainMenu:
                    MainMenu.Render();
                    break;
            }

            CheckErr(SDL.SDL_RenderClear(Renderer)); //Clear 
            CheckErr(SDL.SDL_UpdateTexture(texture, IntPtr.Zero, screen.pixels, screen.pitch));
            CheckErr(SDL.SDL_RenderCopy(Renderer, texture, ref clipRect, IntPtr.Zero)); //Draw texture
            SDL.SDL_RenderPresent(Renderer); //Render

            while (!quit)
            {
                while (SDL.SDL_PollEvent(out SDL.SDL_Event e) != 0)
                {
                    switch (e.type)
                    {
                        case SDL.SDL_EventType.SDL_QUIT:
                            quit = true;
                            break;
                        case SDL.SDL_EventType.SDL_KEYUP:
                            switch (e.key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_HOME:
                                    quit = true;
                                    break;
                            }
                            break;
                    }
                }
            }

            SDL.SDL_Delay(5000);


            SDL.SDL_FreeSurface(screen_ptr);
            SDL.SDL_DestroyTexture(texture);
            SDL.SDL_DestroyRenderer(Renderer);
            SDL.SDL_DestroyWindow(_Window);
            SDL.SDL_Quit();
        }

        public static void CheckErr(int err)
        {
            if (err != 0)
            {
                Console.WriteLine(SDL.SDL_GetError());
            }
        }
    }
}
