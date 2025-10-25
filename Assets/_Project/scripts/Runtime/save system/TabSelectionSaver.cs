public static class TabSelectionSaver
{
    private const string KeyPrefix = "TAB_SELECTION_";

    public static void SaveSelection(string tabKey, int selectedIndex)
    {
        SaveSystem.Save(GetKey(tabKey), selectedIndex);
    }

    public static int LoadSelection(string tabKey)
    {
        return SaveSystem.Load(GetKey(tabKey), -1);
    }

    private static string GetKey(string tabKey)
    {
        return $"{KeyPrefix}{tabKey}";
    }
}