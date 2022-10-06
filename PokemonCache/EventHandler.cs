using PKCache.Screen;
using SDL2;

namespace PKCache
{
    public class SDLEventHandler
    {
        public static ScreenHandler.DisplayState NewEvent(SDL.SDL_Event e, ScreenHandler.DisplayState displayState)
        {
            switch (e.type)
            {
                case SDL.SDL_EventType.SDL_QUIT:
                    displayState = ScreenHandler.DisplayState.Quit;
                    break;
                case SDL.SDL_EventType.SDL_KEYUP:
                    switch (e.key.keysym.sym)
                    {
                        case SDL.SDL_Keycode.SDLK_ESCAPE:
                            displayState = ScreenHandler.DisplayState.Quit;
                            break;
                    }
                    break;
                case SDL.SDL_EventType.SDL_KEYDOWN:
                    switch (displayState)
                    {
                        case ScreenHandler.DisplayState.MainScreen:
                            switch (e.key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_SPACE:
                                    displayState = ScreenHandler.DisplayState.ActionScreen;
                                    break;
                            }
                            break;
                        case ScreenHandler.DisplayState.ActionScreen:
                            switch (e.key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_LCTRL:
                                    displayState = ScreenHandler.DisplayState.MainScreen;
                                    break;
                                case SDL.SDL_Keycode.SDLK_DOWN:
                                    if (ActionScreen.selectionIndex < ActionScreen.maxSelectionIndex)
                                    {
                                        ActionScreen.selectionIndex++;
                                        ScreenHandler.updateDisplay = true;
                                    }
                                    break;
                                case SDL.SDL_Keycode.SDLK_UP:
                                    if (ActionScreen.selectionIndex > 0)
                                    {
                                        ActionScreen.selectionIndex--;
                                        ScreenHandler.updateDisplay = true;
                                    }
                                    break;
                                case SDL.SDL_Keycode.SDLK_SPACE:
                                    switch (ActionScreen.selectionIndex)
                                    {
                                        case 0:
                                            displayState = ScreenHandler.DisplayState.SaveScreen;
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case ScreenHandler.DisplayState.SaveScreen:
                            switch (e.key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_LCTRL:
                                    displayState = ScreenHandler.DisplayState.ActionScreen;
                                    break;
                                case SDL.SDL_Keycode.SDLK_DOWN:
                                    if (SaveScreen.selectionIndex < SaveScreen.maxSelectionIndex)
                                    {
                                        SaveScreen.selectionIndex++;
                                        ScreenHandler.updateDisplay = true;
                                    }
                                    break;
                                case SDL.SDL_Keycode.SDLK_UP:
                                    if (SaveScreen.selectionIndex > 0)
                                    {
                                        SaveScreen.selectionIndex--;
                                        ScreenHandler.updateDisplay = true;
                                    }
                                    break;
                                case SDL.SDL_Keycode.SDLK_SPACE:
                                    SaveHandler.LoadSave(Program.savList[SaveScreen.selectionIndex]);
                                    PKMScreen.pkms = SaveHandler.pkmList!;
                                    displayState = ScreenHandler.DisplayState.PKMScreen;
                                    break;
                            }
                            break;
                        case ScreenHandler.DisplayState.ErrorScreen:
                            switch (e.key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_LCTRL:
                                    displayState = ScreenHandler.DisplayState.ActionScreen;
                                    break;
                                case SDL.SDL_Keycode.SDLK_SPACE:
                                    displayState = ScreenHandler.DisplayState.MainScreen;
                                    break;
                            }
                            break;
                        case ScreenHandler.DisplayState.PKMScreen:
                            switch (e.key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_LCTRL:
                                    displayState = ScreenHandler.DisplayState.SaveScreen;
                                    PKMScreen.selectionIndex = 0;
                                    break;
                                case SDL.SDL_Keycode.SDLK_SPACE:
                                    break;
                                case SDL.SDL_Keycode.SDLK_DOWN:
                                    if (PKMScreen.selectionIndex < PKMScreen.maxSelectionIndex)
                                    {
                                        PKMScreen.selectionIndex++;
                                        ScreenHandler.updateDisplay = true;
                                    }
                                    break;
                                case SDL.SDL_Keycode.SDLK_UP:
                                    if (PKMScreen.selectionIndex > 0)
                                    {
                                        PKMScreen.selectionIndex--;
                                        ScreenHandler.updateDisplay = true;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
            }

            return displayState;
        }
    }
}