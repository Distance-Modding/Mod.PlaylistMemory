using HarmonyLib;

namespace PlaylistMemory.Patches.Assembly_CSharp
{
    /// <summary>
    /// Patch to cache the LastPlaylistName in Singleplayer level selection
    /// when a playlist is clicked and therefor a level is selected in it.
    /// </summary>
    [HarmonyPatch(typeof(LevelGridGrid), "SelectButtonForCurrentPage")]
    public class LevelGridGrid_SelectButtonForCurrentPage
    {
        private static void Postfix(LevelGridGrid __instance)
        {
            LevelGridMenu levelGridMenu = __instance.levelGridMenu_;
            LevelSelectMenuAbstract.DisplayType displayType = levelGridMenu.DisplayType_;

            if (displayType == LevelSelectMenuAbstract.DisplayType.Adventure ||
                displayType == LevelSelectMenuAbstract.DisplayType.Arcade)
            {
                PlaylistNameCache.LastPlaylistName = levelGridMenu.DisplayedEntry_.Playlist_.Name_;
            }
        }
    }
}