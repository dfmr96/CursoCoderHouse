using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryViewController : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryViewObject;
    [SerializeField] private GameObject _contextMenuObject;
    [SerializeField] private GameObject _firstContextMenuOption;
    [SerializeField] private GameObject _firstInventoryOption;
    [SerializeField] private GameObject[] _firstRowSlots;
    [SerializeField] private GameObject _defaultSlot;
    [SerializeField] private GameObject _selector;
    [SerializeField] private ItemSlot _selectedSlot;
    [SerializeField] private TMP_Text _itemNameText;
    [SerializeField] private TMP_Text _itemDescriptionText;

    [SerializeField] private PlayerController _playerController;

    [SerializeField] private List<ItemSlot> _slots;

    [SerializeField] private ScreenFader _screenFader;

    [SerializeField] private List<GameObject> _contextMenuIgnore;

    [SerializeField] private GameObject _promptPanel;
    [SerializeField] private TMP_Text _promptText;
    [SerializeField] private Button _yesBtn;
    [SerializeField] private Button _noBtn;

    [SerializeField] private float stateCooldown = 0;

    public static GameObject lastInteracted;

    private enum State
    {
        Items,
        ContextMenu,
        ItemPickUpPrompt,
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
        EventBus.Instance.onOpenInventory += OpenInventory;
        EventBus.Instance.onCloseInventory += CloseInventory;

        _itemNameText.SetText(_selectedSlot.itemData.Name);
        _itemDescriptionText.SetText(_selectedSlot.itemData.Descripton[0]);
    }

    private void OnDisable()
    {
        EventBus.Instance.onItemPickUp -= OnItemPickedUp;
        EventBus.Instance.onOpenInventory -= OpenInventory;
        EventBus.Instance.onCloseInventory -= CloseInventory;
    }

    private void OnItemPickedUp(ItemData itemData)
    {
        EventBus.Instance.OpenInventory();
        _promptPanel.SetActive(true);
        _state = State.ItemPickUpPrompt;
        EventSystem.current.SetSelectedGameObject(_yesBtn.gameObject);
        _promptText.SetText($"Do you want to take {itemData.Name}?");
        _yesBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            foreach (var slot in _slots)
            {
                if (slot.isEmpty())
                {
                    slot.itemData = itemData;
                    break;
                }
            }
            _yesBtn.onClick.RemoveAllListeners();
            _noBtn.onClick.RemoveAllListeners();
            _promptPanel.SetActive(false);
            Destroy(lastInteracted);
            EventBus.Instance.CloseInventory();
        }
        ));
        _noBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            _promptPanel.SetActive(false);
            _yesBtn.onClick.RemoveAllListeners();
            _noBtn.onClick.RemoveAllListeners();
            EventBus.Instance.CloseInventory();
        }
        ));

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
        _screenFader.FadeToBlack(1f, FadeToUseItemCallback);
        Debug.Log("Data pasada al evento con " + _selectedSlot.itemData.Name);
    }

    public void FadeToUseItemCallback()
    {
        _contextMenuObject.SetActive(false);
        _inventoryViewObject.SetActive(false);
        //EventBus.Instance.UseItem(_selectedSlot.itemData);
        _screenFader.FadeFromBlack(1f, () => EventBus.Instance.UseItem(_selectedSlot.itemData));
        EventBus.Instance.CloseInventory();
        _state = State.MenuClosed;
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
                    _state = State.Items;
                });
            }
            else if (_state == State.Items)
            {
                _screenFader.FadeToBlack(0.25f, () =>
                {
                    EventBus.Instance.CloseInventory();
                });
            }

        }
        if (_state == State.Items)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (EventSystem.current.currentSelectedGameObject.GetComponent<ItemSlot>().itemData != null)
                {
                    _state = State.ContextMenu;
                    _contextMenuObject.gameObject.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(_firstContextMenuOption);
                }
            }

            if (EventSystem.current.currentSelectedGameObject == _firstRowSlots[0] || EventSystem.current.currentSelectedGameObject == _firstRowSlots[1])
            {
                stateCooldown += Time.deltaTime;

                if (Input.GetKeyDown(KeyCode.UpArrow) && stateCooldown > 0.25f)
                {
                    stateCooldown = 0;
                    EventSystem.current.SetSelectedGameObject(_firstInventoryOption);
                    _selector.SetActive(false);
                    _state = State.MenuBar;
                }
            } else
            {
                stateCooldown = 0;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                stateCooldown = 0;
                EventSystem.current.SetSelectedGameObject(_firstInventoryOption);
                _selector.SetActive(false);
                _state = State.MenuBar;
            }
        }

        if (_state == State.ContextMenu)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Escape))
            {
                EventSystem.current.SetSelectedGameObject(_selectedSlot.gameObject);
                _contextMenuObject.SetActive(false);
                _state = State.Items;
            }
        }

        if (_state == State.MenuBar)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                stateCooldown = 0;
                EventSystem.current.SetSelectedGameObject(_defaultSlot);
                _selector.SetActive(true);
                _state = State.Items;
            }

            stateCooldown += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Escape) && stateCooldown > 0.25f)
            {
                stateCooldown = 0;
                _screenFader.FadeToBlack(0.25f, () =>
                {
                    EventBus.Instance.CloseInventory();
                });
            }
        }
    }

    public void OpenInventory()
    {
        _inventoryViewObject.gameObject.SetActive(true);
        _screenFader.FadeFromBlack(0.25f, null);
        EventSystem.current.SetSelectedGameObject(_defaultSlot);
        _selector.SetActive(true);
        //_state = State.Items;
    }

    public void CloseInventory()
    {
        _inventoryViewObject.gameObject.SetActive(false);
        _screenFader.FadeFromBlack(0.25f, null);
        _state = State.MenuClosed;
    }
}