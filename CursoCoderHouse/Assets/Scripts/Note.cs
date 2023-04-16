using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class Note : MonoBehaviour, IInteractable
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private GameObject _noteViewer;
    [SerializeField] private TMP_Text _header;
    [SerializeField] private TMP_Text _pageField;
    [SerializeField] private Button _leftBtn;
    [SerializeField] private Button _rightBtn;
    [SerializeField] private Image _background;
    [SerializeField] private NoteItemData _itemData;
    [SerializeField] private InventoryViewController _inventoryViewController;

    public void Interact(ItemData item)
    {
        Time.timeScale = 0f;
        _noteViewer.SetActive(true);
        _inventoryViewController.SetState(InventoryState.NoteViewer);
        InventoryViewController.lastInteracted = this.gameObject;
        _header.SetText(_itemData.Header);
        _pageField.SetText(_itemData.Pages);
        _background.sprite = _itemData.Background;
    }
}
