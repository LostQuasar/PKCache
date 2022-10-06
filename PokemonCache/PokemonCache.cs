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
        internal static string[] savList = Array.Empty<string>();
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
                    savPath = "/mnt/SDCARD/Saves/CurrentProfile/saves/gpSP/";
                    break;
                case RuntimeEnv.Desktop:
                    savPath = basePath;
                    break;
            }

            SaveUtil.GetSavesFromFolder(savPath, true, out IEnumerable<string> files, true);
            foreach (string file in files)
            {
                if (file.ToLower().Contains("pokemon"))
                {
                    savList = savList.Append(file).ToArray();
                }
            }
            
            ScreenHandler.Init();
            ScreenHandler.DisplayLoop();
            ScreenHandler.Quit();
        }
    }
}
