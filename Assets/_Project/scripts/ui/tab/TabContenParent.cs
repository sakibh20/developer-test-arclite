using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class TabContentParent : MonoBehaviour
{
    private readonly List<TabContentItem> _spawnedItems = new();

    public void Setup(List<TabContentItem> itemPrefab, Renderer targetRenderer)
    {
        ToggleGroup group = GetComponent<ToggleGroup>();
        
        for (int i = 0; i < itemPrefab.Count; i++)
        {
            var itemInstance = Instantiate(itemPrefab[i], transform);
            itemInstance.Initialize(targetRenderer, group);
            _spawnedItems.Add(itemInstance);
        }
    }
}