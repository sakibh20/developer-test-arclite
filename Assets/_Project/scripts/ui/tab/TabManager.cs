using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    [SerializeField] private List<TabButton> allTabButtonPrefabs;
    [SerializeField] private ToggleGroup tabButtonParent;
    [SerializeField] private Transform tabContentParent;
    [SerializeField] private TabContentParent tabItemParentPrefab;

    private readonly List<TabButton> _instantiatedTabButtons = new();

    private void Awake()
    {
        GenerateTabs();
    }

    private void OnDestroy()
    {
        foreach (var tabPrefab in _instantiatedTabButtons)
        {
            tabPrefab.OnTabSelected -= HandleTabSelected;
        }
    }

    private void GenerateTabs()
    {
        if (allTabButtonPrefabs == null || allTabButtonPrefabs.Count == 0)
        {
            Debug.LogWarning("TabManager: No TabButton prefabs assigned!");
            return;
        }

        foreach (var tabPrefab in allTabButtonPrefabs)
        {
            // Instantiate tab button
            TabButton newTab = Instantiate(tabPrefab, tabButtonParent.transform);
            newTab.Initialize(tabButtonParent, tabContentParent, tabItemParentPrefab);

            // Subscribe to on-click or toggle events
            newTab.OnTabSelected += HandleTabSelected;

            _instantiatedTabButtons.Add(newTab);
        }

        // Activate the first tab by default
        if (_instantiatedTabButtons.Count > 0)
        {
            _instantiatedTabButtons[0].Select();
        }
    }

    private void HandleTabSelected(TabButton selectedTab)
    {
        Debug.Log("HandleTabSelected");
        // Deactivate others
        foreach (var tab in _instantiatedTabButtons)
        {
            if (tab != selectedTab)
                tab.Deselect();
        }
    }
}