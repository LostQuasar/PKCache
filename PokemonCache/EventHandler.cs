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
                                        case 1:
                                            PKMScreen.pkms = CacheHandler.pkmList;
                                            displayState = ScreenHandler.DisplayState.CacheScreen;
                                            break;
                                        case 2:
                                            displayState = ScreenHandler.DisplayState.ExtrasScreen;
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
                                    SaveHandler.LoadSave(SaveHandler.saveDict[SaveHandler.saveDict.Keys.ToArray()[SaveScreen.selectionIndex]]);
                                    SaveHandler.savPath = SaveHandler.saveDict.Values.ToArray()[SaveScreen.selectionIndex];
                                    PKMScreen.pkms = SaveHandler.pkmList!;
                                    SaveHandler.CreateBackup(SaveHandler.savPath);
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
                                    SaveHandler.selectedPKM = SaveHandler.pkmList?[PKMScreen.selectionIndex];
                                    PKMScreen.selectionIndex = 0;
                                     displayState = ScreenHandler.DisplayState.LocationScreen;
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
                        case ScreenHandler.DisplayState.LocationScreen:
                            switch (e.key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_LCTRL:
                                    displayState = ScreenHandler.DisplayState.PKMScreen;
                                    PKMScreen.selectionIndex = 0;
                                    break;
                                case SDL.SDL_Keycode.SDLK_SPACE:
                                    switch (LocationScreen.selectionIndex)
                                    {
                                        case 0:
                                            displayState = ScreenHandler.DisplayState.ExportSaveScreen;
                                            break;
                                        case 1:
                                            SaveHandler.MovePKM(SaveHandler.PKMTransfer.FromSavToCache, SaveHandler.selectedPKM);
                                            displayState = ScreenHandler.DisplayState.SuccessScreen;
                                            break;
                                    }
                                    break;
                                case SDL.SDL_Keycode.SDLK_DOWN:
                                    if (LocationScreen.selectionIndex < LocationScreen.maxSelectionIndex)
                                    {
                                        LocationScreen.selectionIndex++;
                                        ScreenHandler.updateDisplay = true;
                                    }
                                    break;
                                case SDL.SDL_Keycode.SDLK_UP:
                                    if (LocationScreen.selectionIndex > 0)
                                    {
                                        LocationScreen.selectionIndex--;
                                        ScreenHandler.updateDisplay = true;
                                    }
                                    break;
                            }
                            break;
                        case ScreenHandler.DisplayState.SuccessScreen:
                            switch (e.key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_SPACE:
                                    displayState = ScreenHandler.DisplayState.MainScreen;
                                    break;
                            }
                            break;
                        case ScreenHandler.DisplayState.ExportSaveScreen:
                            switch (e.key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_LCTRL:
                                    displayState = ScreenHandler.DisplayState.LocationScreen;
                                    PKMScreen.selectionIndex = 0;
                                    break;
                                case SDL.SDL_Keycode.SDLK_SPACE:
                                    break;
                                case SDL.SDL_Keycode.SDLK_DOWN:
                                    if (ExportSaveScreen.selectionIndex < ExportSaveScreen.maxSelectionIndex)
                                    {
                                        ExportSaveScreen.selectionIndex++;
                                        ScreenHandler.updateDisplay = true;
                                    }
                                    break;
                                case SDL.SDL_Keycode.SDLK_UP:
                                    if (ExportSaveScreen.selectionIndex > 0)
                                    {
                                        ExportSaveScreen.selectionIndex--;
                                        ScreenHandler.updateDisplay = true;
                                    }
                                    break;
                            }
                            break;
                        case ScreenHandler.DisplayState.CacheScreen:
                            switch (e.key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_LCTRL:
                                    displayState = ScreenHandler.DisplayState.ActionScreen;
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
                        case ScreenHandler.DisplayState.ExtrasScreen:
                            switch (e.key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_LCTRL:
                                    displayState = ScreenHandler.DisplayState.ActionScreen;
                                    break;
                                case SDL.SDL_Keycode.SDLK_SPACE:
                                    switch (ExtrasScreen.selectionIndex)
                                    {
                                        case 0:
                                            break;
                                        case 1:
                                            break;
                                        case 2:
                                            displayState = ScreenHandler.DisplayState.GiftScreen;
                                            break;
                                    }
                                    break;
                                case SDL.SDL_Keycode.SDLK_DOWN:
                                    if (ExtrasScreen.selectionIndex < ExtrasScreen.maxSelectionIndex)
                                    {
                                        ExtrasScreen.selectionIndex++;
                                        ScreenHandler.updateDisplay = true;
                                    }
                                    break;
                                case SDL.SDL_Keycode.SDLK_UP:
                                    if (ExtrasScreen.selectionIndex > 0)
                                    {
                                        ExtrasScreen.selectionIndex--;
                                        ScreenHandler.updateDisplay = true;
                                    }
                                    break;
                            }
                            break;
                        case ScreenHandler.DisplayState.GiftScreen:
                            switch (e.key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_LCTRL:
                                    displayState = ScreenHandler.DisplayState.ExtrasScreen;
                                    break;
                                case SDL.SDL_Keycode.SDLK_SPACE:
                                    break;
                                case SDL.SDL_Keycode.SDLK_RIGHT:
                                    if (GiftScreen.selectionIndex < GiftScreen.maxSelectionIndex)
                                    {
                                        GiftScreen.selectionIndex++;
                                        ScreenHandler.updateDisplay = true;
                                    }
                                    break;
                                case SDL.SDL_Keycode.SDLK_LEFT:
                                    if (GiftScreen.selectionIndex > 0)
                                    {
                                        GiftScreen.selectionIndex--;
                                        ScreenHandler.updateDisplay = true;
                                    }
                                    break;
                                case SDL.SDL_Keycode.SDLK_UP:
                                    if (GiftScreen.value[GiftScreen.selectionIndex] < 0xF)
                                    {
                                        GiftScreen.value[GiftScreen.selectionIndex]++;
                                        ScreenHandler.updateDisplay = true;
                                    }
                                    break;
                                case SDL.SDL_Keycode.SDLK_DOWN:
                                    if (GiftScreen.value[GiftScreen.selectionIndex] > 0x0)
                                    {
                                        GiftScreen.value[GiftScreen.selectionIndex]--;
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