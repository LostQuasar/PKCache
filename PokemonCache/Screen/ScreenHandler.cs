using SDL2;
using System.Runtime.InteropServices;

namespace PKCache.Screen
{
    public class ScreenHandler
    {
        private static DisplayState displayState;
        private static DisplayState lastDisplayState;
        public static readonly int SCREEN_WIDTH = 640;
        public static readonly int SCREEN_HEIGHT = 480;
        public static readonly int BPP = 32;
        private static IntPtr _Window = IntPtr.Zero;
        internal static IntPtr Renderer;
        internal static IntPtr screen_ptr;
        private static IntPtr texture;
        internal static SDL.SDL_Rect clipRect;
        private static SDL.SDL_Surface screen;
        internal static bool updateDisplay = false;

        public enum DisplayState
        {
            None,
            MainScreen,
            ActionScreen,
            SaveScreen,
            ErrorScreen,
            PKMScreen,
            LocationScreen,
            SuccessScreen,
            CacheScreen,
            Quit,
            ExportSaveScreen,
            ExtrasScreen,
            GiftScreen
        }

        public static void Init()
        {
            CheckErr(SDL.SDL_Init(SDL.SDL_INIT_VIDEO));
            CheckErr(SDL_ttf.TTF_Init());

            UI_Elements.Init();
            PKMSpriteData.Init();

            displayState = DisplayState.MainScreen;
            lastDisplayState = DisplayState.None;
            _Window = SDL.SDL_CreateWindow("main", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, SCREEN_WIDTH, SCREEN_HEIGHT, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
            Renderer = SDL.SDL_CreateRenderer(_Window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            screen_ptr = SDL.SDL_CreateRGBSurface(0, SCREEN_WIDTH, SCREEN_HEIGHT, BPP, 0, 0, 0, 0);
            screen = Marshal.PtrToStructure<SDL.SDL_Surface>(screen_ptr);
            texture = SDL.SDL_CreateTexture(Renderer, SDL.SDL_PIXELFORMAT_ARGB8888, (int)SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING, SCREEN_WIDTH, SCREEN_HEIGHT);
            SDL.SDL_GetClipRect(screen_ptr, out clipRect);
            MainScreen.Render();
        }


        public static void DisplayLoop()
        {
            while (displayState != DisplayState.Quit)
            {
                if (updateDisplay | (lastDisplayState != displayState) | Program.errorOccured.Item1)
                {
                    CheckErr(SDL.SDL_FillRect(screen_ptr, ref clipRect, SDL.SDL_MapRGB(screen.format, 0x10, 0xF0, 0xA0))); //Draw BG
                    if (Program.errorOccured.Item1)
                    {
                        displayState = DisplayState.ErrorScreen;
                    }
                    switch (displayState)
                    {
                        case DisplayState.MainScreen:
                            MainScreen.Render();
                            break;
                        case DisplayState.ActionScreen:
                            ActionScreen.Render();
                            break;
                        case DisplayState.SaveScreen:
                            SaveScreen.Render();
                            break;
                        case DisplayState.ErrorScreen:
                            ErrorScreen.Render();
                            Program.errorOccured = Tuple.Create(false, "");
                            break;
                        case DisplayState.PKMScreen:
                            PKMScreen.Render();
                            break;
                        case DisplayState.LocationScreen:
                            LocationScreen.Render();
                            break;
                        case DisplayState.SuccessScreen:
                            SuccessScreen.Render();
                            break;
                        case DisplayState.CacheScreen:
                            PKMScreen.Render();
                            break;
                        case DisplayState.ExportSaveScreen:
                            ExportSaveScreen.Render();
                            break;
                        case DisplayState.ExtrasScreen:
                            ExtrasScreen.Render();
                            break;
                        case DisplayState.GiftScreen:
                            GiftScreen.Render();
                            break;
                    }

                    if (lastDisplayState == DisplayState.SuccessScreen)
                    {
                        CacheHandler.Init();
                    }

                    CheckErr(SDL.SDL_RenderClear(Renderer)); //Clear 
                    CheckErr(SDL.SDL_UpdateTexture(texture, IntPtr.Zero, screen.pixels, screen.pitch));
                    CheckErr(SDL.SDL_RenderCopy(Renderer, texture, ref clipRect, IntPtr.Zero)); //Draw texture
                    SDL.SDL_RenderPresent(Renderer); //Render

                    updateDisplay = false;
                    lastDisplayState = displayState;
                }

                while (SDL.SDL_PollEvent(out SDL.SDL_Event e) != 0)
                {
                    displayState = SDLEventHandler.NewEvent(e, displayState);
                }
            }
        }

        public static void CheckErr(int err)
        {
            if (err != 0)
            {
                Console.WriteLine(SDL.SDL_GetError());
            }
        }

        public static void Quit()
        {
            UI_Elements.Quit();
            SDL.SDL_FreeSurface(screen_ptr);
            SDL.SDL_DestroyTexture(texture);
            SDL.SDL_DestroyRenderer(Renderer);
            SDL.SDL_DestroyWindow(_Window);
            SDL.SDL_Quit();
        }
    }
}
