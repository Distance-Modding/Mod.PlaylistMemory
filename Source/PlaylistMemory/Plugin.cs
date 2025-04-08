using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using PlaylistMemory.Properties;

namespace PlaylistMemory
{
    [BepInPlugin(ModInfo.ID, ModInfo.NAME, ModInfo.VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal new static ManualLogSource Logger { get; private set; }

        private void Awake()
        {
            Logger = base.Logger;

            Harmony harmony = new Harmony(ModInfo.ID);
            harmony.PatchAll();
        }
    }
}
