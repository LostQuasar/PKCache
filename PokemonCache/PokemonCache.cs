using PKHeX.Core;
using PKCache.Screen;

namespace PKCache
{
    class Program
    {
        public enum RuntimeEnv
        {
            MiyooMini,
            Desktop
        }

        public static string basePath = "";
        internal static Tuple<bool,string> errorOccured = Tuple.Create(false, "");
        private static RuntimeEnv env;
        public static string savPath = "";

        static void Main()
        {
            basePath = Directory.GetCurrentDirectory() + "/res/";
            env = basePath.Contains("SDCARD") ? RuntimeEnv.MiyooMini : RuntimeEnv.Desktop;

            switch (env)
            {
                case RuntimeEnv.MiyooMini:
                    savPath = "/mnt/SDCARD/Saves/CurrentProfile/saves/";
                    break;
                case RuntimeEnv.Desktop:
                    savPath = basePath;
                    break;
            }

            ScreenHandler.Init();
            SaveHandler.Init();
            CacheHandler.Init();
            ScreenHandler.DisplayLoop();
            ScreenHandler.Quit();
        }
    }
}
