using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class TabButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private string title;
    [SerializeField] private List<TabContentItem> contentItemPrefabs;
    [SerializeField] private Renderer targetRenderer;

    [Header("Colors")]
    [SerializeField] private Color32 activeColor = new(255, 70, 70, 255);
    [SerializeField] private Color32 inactiveColor = new(200, 200, 200, 255);

    public event Action<TabButton> OnTabSelected;

    private Toggle _toggle;
    private Transform _tabContentParent;
    private TabContentParent _tabItemParentPrefab;
    private TabContentParent _instantiatedContentParent;

    private bool _isInitialized;

    public void Initialize(ToggleGroup toggleGroup, Transform contentParent, TabContentParent tabItemParentPrefab)
    {
        if (_isInitialized) return;
        
        titleText.SetText(title);

        _toggle = GetComponent<Toggle>();
        _toggle.group = toggleGroup;
        _tabContentParent = contentParent;
        _tabItemParentPrefab = tabItemParentPrefab;

        Debug.Log("AddListener");
        _toggle.onValueChanged.AddListener(OnToggleChanged);
        _isInitialized = true;
    }

    private void OnToggleChanged(bool isOn)
    {
        titleText.color = isOn ? activeColor : inactiveColor;

        if (isOn)
        {
            if (_instantiatedContentParent == null)
            {
                _instantiatedContentParent = Instantiate(_tabItemParentPrefab, _tabContentParent);
                _instantiatedContentParent.Setup(contentItemPrefabs, GetComponent<Renderer>());
            }

            _instantiatedContentParent.gameObject.SetActive(true);
            OnTabSelected?.Invoke(this);
        }
        else
        {
            if (_instantiatedContentParent != null)
                _instantiatedContentParent.gameObject.SetActive(false);
        }
    }

    [ContextMenu("Select")]
    public void Select()
    {
        Debug.Log("Select");
        _toggle.isOn = true;
    }
    public void Deselect()
    {
        _toggle.isOn = false;
    }
}