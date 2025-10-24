using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class TabContentParent : MonoBehaviour
{
    private readonly List<TabContentItem> _spawnedItems = new();
    private string _tabKey;

    public void Setup(List<TabContentItem> itemPrefabs, TabContext context, string tabKey)
    {
        _tabKey = tabKey;
        ToggleGroup group = GetComponent<ToggleGroup>();

        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            int index = i;
            var itemInstance = Instantiate(itemPrefabs[i], transform);
            itemInstance.Initialize(context, group);
            
            itemInstance.OnItemSelected += () => OnItemSelected(index);
            _spawnedItems.Add(itemInstance);
        }

        int savedIndex = TabSelectionSaver.LoadSelection(_tabKey);
        if (savedIndex >= 0 && savedIndex < _spawnedItems.Count)
        {
            _spawnedItems[savedIndex].Select();
        }
    }

    private void OnItemSelected(int index)
    {
        TabSelectionSaver.SaveSelection(_tabKey, index);
    }
}