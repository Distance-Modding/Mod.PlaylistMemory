using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace PlaylistMemory.Patches.Assembly_CSharp
{
    /// <summary>
    /// Patch to consider the last played playlist when trying to find and select the previously played or selected level.
    /// </summary>
    [HarmonyPatch(typeof(LevelGridMenu), "CreateEntries")]
    internal class LevelGridMenu_CreateEntries
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            Plugin.Logger.LogInfo($"Transpiling LevelGridMenu.CreateEntries().");

            MethodInfo oldFindIndexMethod = AccessTools.Method(
                typeof(List<LevelGridMenu.PlaylistEntry>),
                nameof(List<LevelGridMenu.PlaylistEntry>.FindIndex),
                new[] { typeof(Predicate<LevelGridMenu.PlaylistEntry>) });

            MethodInfo newFindIndexMethod = AccessTools.Method(
                typeof(LevelGridMenu_CreateEntries),
                nameof(FindPlaylistLevelIndex));

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Callvirt && instruction.operand == oldFindIndexMethod)
                {
                    Plugin.Logger.LogInfo("Replacing FindIndex method.");
                    instruction.opcode = OpCodes.Call;
                    instruction.operand = newFindIndexMethod;
                }

                yield return instruction;
            }
        }

        /// <summary>
        /// Tries to find the index of a level in the last played playlist.
        /// As a fallback, the index of the level from any playlist will be returned. (Vanilla behavior)
        /// </summary>
        private static int FindPlaylistLevelIndex(List<LevelGridMenu.PlaylistEntry> list, Predicate<LevelGridMenu.PlaylistEntry> predicate)
        {
            string lastPlaylistName = PlaylistNameCache.LastPlaylistName;
            bool noLastPlaylistName = string.IsNullOrEmpty(lastPlaylistName);

            int fallbackIndex = -1;
            for (int i = 0; i < list.Count; i++)
            {
                LevelGridMenu.PlaylistEntry playlistEntry = list[i];
                if (!predicate.Invoke(playlistEntry))
                    continue;

                if (noLastPlaylistName || playlistEntry.Playlist_.Name_ == lastPlaylistName)
                    return i;

                if (fallbackIndex == -1)
                    fallbackIndex = i;
            }

            return fallbackIndex;
        }
    }
}
