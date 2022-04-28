using MelonLoader;
using System.Diagnostics;

namespace GamePriority
{
    public class GamePriorityChanger : MelonMod
    {
        public static class BuildInfo
        {
            public const string Name = "GamePriority";
            public const string Author = "Lily";
            public const string Company = null;
            public const string Version = "1.1.2";
            public const string DownloadLink = "https://github.com/MintLily/GamePriority/";
        }

        public static GamePriorityChanger Instance { get; private set; }

        public static MelonPreferences_Category category;
        public static MelonPreferences_Entry<bool> setGamePriority;
        public override void OnApplicationStart()
        {
            category = MelonPreferences.CreateCategory("GamePriority", "Game Priority");
            setGamePriority = category.CreateEntry("SetGamePriorityToHigh", false, "Set game priority to High");

            ApplyChanges();
        }

        public override void OnPreferencesSaved()
        {
            ApplyChanges();
        }

        private static void ApplyChanges()
        {
            bool highPriority = setGamePriority.Value;
            if (!highPriority)
            {
                using (Process p = Process.GetCurrentProcess())
                    p.PriorityClass = ProcessPriorityClass.Normal;
                Instance.LoggerInstance.Msg($"Set game's process priority to: Normal");
            }
            else if (highPriority)
            {
                using (Process p = Process.GetCurrentProcess())
                    p.PriorityClass = ProcessPriorityClass.High;
                Instance.LoggerInstance.Msg($"Set game's process priority to: High");
            }
        }
    }
}
