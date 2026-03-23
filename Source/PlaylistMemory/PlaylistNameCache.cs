namespace PlaylistMemory
{
    internal static class PlaylistNameCache
    {
        private const string LastPlaylistNameConfigKey = "Last Playlist Name";

        internal static string LastPlaylistName
        {
            get => G.Sys.GameData_.GetString(LastPlaylistNameConfigKey);
            set => G.Sys.GameData_.SetString(LastPlaylistNameConfigKey, value);
        }
    }
}
