using HarmonyLib;

namespace PlaylistMemory.Patches.Assembly_CSharp
{
    /// <summary>
    /// Patch to set the NextLevelPath in Singleplayer level selection
    /// when a level is selected (not clicked).
    /// </summary>
    [HarmonyPatch(typeof(LevelGridMenu), nameof(LevelGridMenu.OnLevelEntrySelected))]
    public class LevelGridMenu_OnLevelEntrySelected
    {
        private static void Postfix(LevelGridMenu __instance)
        {
            LevelSelectMenuAbstract.DisplayType instanceDisplayType = __instance.DisplayType_;

            if (instanceDisplayType == LevelSelectMenuAbstract.DisplayType.Arcade ||
                instanceDisplayType == LevelSelectMenuAbstract.DisplayType.Adventure)
            {
                __instance.SetLevelInfoIfNeeded();
            }
        }
    }
}