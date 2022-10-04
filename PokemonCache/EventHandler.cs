
using SDL2;
using static PokemonCache.PokemonCache;

namespace PokemonCache
{
	public class SDL_EventHandler
	{
        internal static DisplayState NewEvent(SDL.SDL_Event e, DisplayState displayState)
		{
            switch (e.type)
            {
                case SDL.SDL_EventType.SDL_QUIT:
                    displayState = DisplayState.Quit;
                    break;
                case SDL.SDL_EventType.SDL_KEYUP:
                    switch (e.key.keysym.sym)
                    {
                        case SDL.SDL_Keycode.SDLK_ESCAPE:
                            displayState = DisplayState.Quit;
                            break;
                    }
                    break;
                case SDL.SDL_EventType.SDL_KEYDOWN:
                    switch (displayState)
                    {
                        case DisplayState.MainMenu:
                            switch (e.key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_SPACE:
                                    displayState = DisplayState.ActionMenu;
                                    break;
                            }
                            break;
                        case DisplayState.ActionMenu:
                            switch (e.key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_LCTRL:
                                    displayState = DisplayState.MainMenu;
                                    break;
                                case SDL.SDL_Keycode.SDLK_DOWN:
                                    if (ActionMenu.selectionIndex < ActionMenu.maxSelectionIndex)
                                    {
                                        ActionMenu.selectionIndex++;
                                        updateDisplay = true;
                                    }
                                    break;
                                case SDL.SDL_Keycode.SDLK_UP:
                                    if (ActionMenu.selectionIndex > 0)
                                    {
                                        ActionMenu.selectionIndex--;
                                        updateDisplay = true;
                                    }
                                    break;
                                case SDL.SDL_Keycode.SDLK_SPACE:
                                    switch (ActionMenu.selectionIndex)
                                    {
                                        case 0:
                                            displayState = DisplayState.SaveMenu;
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case DisplayState.SaveMenu:
                            switch (e.key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_LCTRL:
                                    displayState = DisplayState.ActionMenu;
                                    break;
                                case SDL.SDL_Keycode.SDLK_DOWN:
                                    if (SaveMenu.selectionIndex < SaveMenu.maxSelectionIndex)
                                    {
                                        SaveMenu.selectionIndex++;
                                        updateDisplay = true;
                                    }
                                    break;
                                case SDL.SDL_Keycode.SDLK_UP:
                                    if (SaveMenu.selectionIndex > 0)
                                    {
                                        SaveMenu.selectionIndex--;
                                        updateDisplay = true;
                                    }
                                    break;
                                case SDL.SDL_Keycode.SDLK_SPACE:
                                    SaveHandler.LoadSave(savList[SaveMenu.selectionIndex]);
                                    displayState = DisplayState.PKMMenu;
                                    break;
                            }
                            break;
                        case DisplayState.ErrorMenu:
                            switch (e.key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_LCTRL:
                                    displayState = DisplayState.ActionMenu;
                                    break;
                                case SDL.SDL_Keycode.SDLK_SPACE:
                                    displayState = DisplayState.MainMenu;
                                    break;
                            }
                            break;
                        case DisplayState.PKMMenu:
                            switch (e.key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_LCTRL:
                                    displayState = DisplayState.SaveMenu;
                                    PKMMenu.selectionIndex = 0;
                                    break;
                                case SDL.SDL_Keycode.SDLK_SPACE:
                                    break;
                                case SDL.SDL_Keycode.SDLK_DOWN:
                                    if (PKMMenu.selectionIndex < PKMMenu.maxSelectionIndex)
                                    {
                                        PKMMenu.selectionIndex++;
                                        updateDisplay = true;
                                    }
                                    break;
                                case SDL.SDL_Keycode.SDLK_UP:
                                    if (PKMMenu.selectionIndex > 0)
                                    {
                                        PKMMenu.selectionIndex--;
                                        updateDisplay = true;
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