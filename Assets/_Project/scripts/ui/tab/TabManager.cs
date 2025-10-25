using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    [SerializeField] private List<TabSetupData> tabSetups;
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
        if (tabSetups == null || tabSetups.Count == 0)
        {
            Debug.LogWarning("TabManager: No tab setups assigned!");
            return;
        }

        foreach (var setup in tabSetups)
        {
            TabButton newTab = Instantiate(setup.tabPrefab, tabButtonParent.transform);
            
            newTab.Initialize(tabButtonParent, tabContentParent, tabItemParentPrefab, new TabContext(setup.targetRenderer));

            newTab.OnTabSelected += HandleTabSelected;
            _instantiatedTabButtons.Add(newTab);
        }

        if (_instantiatedTabButtons.Count > 0)
            _instantiatedTabButtons[0].Select();
    }

    private void HandleTabSelected(TabButton selectedTab)
    {
        foreach (var tab in _instantiatedTabButtons)
        {
            if (tab != selectedTab)
                tab.Deselect();
        }
    }
}

[System.Serializable]
public class TabSetupData
{
    public TabButton tabPrefab;
    public Renderer targetRenderer;
}