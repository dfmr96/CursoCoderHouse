using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryViewController : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryViewObject;
    [SerializeField] private GameObject _contextMenuObject;
    [SerializeField] private GameObject _firstContextMenuOption;
    [SerializeField] private GameObject _defaultSlot;
    [SerializeField] private ItemSlot _selectedSlot;
    [SerializeField] private TMP_Text _itemNameText;
    [SerializeField] private TMP_Text _itemDescriptionText;

    [SerializeField] private PlayerController _playerController;

    [SerializeField] private List<ItemSlot> _slots;

    [SerializeField] private ScreenFader _screenFader;

    [SerializeField] private List<GameObject> _contextMenuIgnore;

    private enum State
    {
        Items,
        ContextMenu,
        MenuBar,
        MenuClosed
    }

    [SerializeField] private State _state;

    private void Awake()
    {
        _state = State.MenuClosed;
        EventSystem.current.SetSelectedGameObject(_defaultSlot);
    }

    private void OnEnable()
    {
        EventBus.Instance.onItemPickUp += OnItemPickedUp;
    }

    private void OnDisable()
    {
        EventBus.Instance.onItemPickUp -= OnItemPickedUp;
    }

    private void OnItemPickedUp(ItemData itemData)
    {
        foreach (var slot in _slots)
        {
            if (slot.isEmpty())
            {
                slot.itemData = itemData;
                break;
            }
        }
    }

    public void OnSlotSelected(ItemSlot selectedSlot)
    {
        if (selectedSlot.itemData == null)
        {
            _itemNameText.ClearMesh();
            _itemDescriptionText.ClearMesh();
            return;
        }
        _selectedSlot = selectedSlot;
        _itemNameText.SetText(selectedSlot.itemData.Name);
        _itemDescriptionText.SetText(selectedSlot.itemData.Descripton[0]);
    }

    public void UseItem()
    {
        EventBus.Instance.UseItem(_selectedSlot.itemData);
        Debug.Log("Data pasada al evento con " + _selectedSlot.itemData.Name);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {

            if (_state == State.MenuClosed)
            {
                _screenFader.FadeToBlack(0.25f, () =>
                {
                    EventBus.Instance.OpenInventory();
                    _inventoryViewObject.gameObject.SetActive(true);
                    _screenFader.FadeFromBlack(0.25f, null);
                    EventSystem.current.SetSelectedGameObject(_defaultSlot);
                    _state = State.Items;
                });
            }
            else if (_state == State.Items)
            {
                _screenFader.FadeToBlack(0.25f, () =>
                {
                    EventBus.Instance.CloseInventory();
                    _inventoryViewObject.gameObject.SetActive(false);
                    _screenFader.FadeFromBlack(0.25f, null);
                    _state = State.MenuClosed;
                });
            }

        }
        if (Input.GetKeyDown(KeyCode.E) && _state == State.Items)
        {
            //if (EventSystem.current.currentSelectedGameObject.TryGetComponent<ItemSlot>(out var slot))
            {
                _state = State.ContextMenu;
                _contextMenuObject.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(_firstContextMenuOption);
            }
        }

        if (_state == State.ContextMenu)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.D)))
            {
                EventSystem.current.SetSelectedGameObject(_selectedSlot.gameObject);
                _contextMenuObject.SetActive(false);
                _state = State.Items;
            }
        }
    }
}