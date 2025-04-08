using HarmonyLib;

namespace PlaylistMemory.Patches.Assembly_CSharp
{
    /// <summary>
    /// Patch to cache the LastPlaylistName in Multiplayer level selection
    /// when a level was clicked.
    /// </summary>
    [HarmonyPatch(typeof(LevelGridMenu), nameof(LevelGridMenu.OnGridCellClicked))]
    public class LevelGridMenu_OnGridCellClicked
    {
        private static void Postfix(LevelGridMenu __instance)
        {
            if (__instance.DisplayType_ == LevelSelectMenuAbstract.DisplayType.GameLobby)
                PlaylistNameCache.LastPlaylistName = __instance.DisplayedEntry_.Playlist_.Name_;
        }
    }
}