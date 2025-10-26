using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class TabContentParent : MonoBehaviour
{
    private readonly List<TabContentItem> _spawnedItems = new();
    private string _tabKey;

    private ToggleGroup _group;
    private ScrollRect _scrollRect;
    private RectTransform _contentArea;

    private void Awake()
    {
        _scrollRect = GetComponentInParent<ScrollRect>();
        _contentArea = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        _scrollRect.content = _contentArea;
    }

    public void Setup(List<TabContentItem> itemPrefabs, TabContext context, string tabKey)
    {
        _tabKey = tabKey;
        _group = GetComponent<ToggleGroup>();

        // Generate content items and init them
        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            int index = i;
            var itemInstance = Instantiate(itemPrefabs[i], transform);
            itemInstance.Initialize(context, _group);
            
            itemInstance.OnItemSelected += () => OnItemSelected(index);
            _spawnedItems.Add(itemInstance);
        }

        // Load saved preferences
        int savedIndex = TabSelectionSaver.LoadSelection(_tabKey);
        if (savedIndex >= 0 && savedIndex < _spawnedItems.Count)
        {
            _spawnedItems[savedIndex].Select();
        }
    }
    
    // TODO: make scroll system virtualized or recycled
    // I noticed the instruction to make items virtualized or recycled
    // Didn't do that yet. It had some difficulties that I couldn't solve in this short time.

    // Handle On Select
    private void OnItemSelected(int index)
    {
        TabSelectionSaver.SaveSelection(_tabKey, index);
    }
}