using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum InventoryState
{
    Items,
    ContextMenu,
    ItemPickUpPrompt,
    NoteViewer,
    MenuBar,
    MenuClosed
}

public class InventoryViewController : MonoBehaviour
{
    [Header("Core")]
    public static GameObject lastInteracted;
    [SerializeField] private GameObject lastInteractedDebug;
    [SerializeField] private InventoryState _state;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _inventoryViewObject;
    [SerializeField] private ScreenFader _screenFader;
    [SerializeField] private GameObject _noteViewer;
    [SerializeField] private float stateCooldown = 0;

    [Space(20)]
    [Header("Inventory Slots")]
    [SerializeField] private GameObject _defaultSlot;
    [SerializeField] private ItemSlot _selectedSlot;
    [SerializeField] private List<ItemSlot> _slots;
    [SerializeField] private GameObject _firstInventoryOption;
    [SerializeField] private GameObject[] _firstRowSlots;
    [SerializeField] private GameObject _selector;

    [Space(20)]
    [Header("Context Menu")]
    [SerializeField] private GameObject _contextMenuObject;
    [SerializeField] private Button _firstInteractable;
    [SerializeField] private GameObject _firstContextMenuOption;
    [SerializeField] private List<Button> _menuContextBtns;
    [SerializeField] private List<Button> _menuContextInteractableBtns;
    [SerializeField] private List<GameObject> _contextMenuIgnore;
    [SerializeField] private Button _equipBtn;
    [SerializeField] private Button _checkBtn;
    [SerializeField] private Button _combineBtn;
    [SerializeField] private Button _useBtn;

    [Space(20)]
    [Header("PickUp Prompt")]
    [SerializeField] private GameObject _promptPanel;
    [SerializeField] private TMP_Text _promptText;
    [SerializeField] private Button _yesBtn;
    [SerializeField] private Button _noBtn;

    [Space(20)]
    [Header("Item Description")]
    [SerializeField] private TMP_Text _itemNameText;
    [SerializeField] private TMP_Text _itemDescriptionText;


    private void Awake()
    {
        _state = InventoryState.MenuClosed;
        EventSystem.current.SetSelectedGameObject(_defaultSlot);
    }

    private void OnEnable()
    {
        EventBus.Instance.onItemPickUp += OnItemPickedUp;
        EventBus.Instance.onOpenInventory += OpenInventory;
        EventBus.Instance.onCloseInventory += CloseInventory;

    }

    private void OnDisable()
    {
        EventBus.Instance.onItemPickUp -= OnItemPickedUp;
        EventBus.Instance.onOpenInventory -= OpenInventory;
        EventBus.Instance.onCloseInventory -= CloseInventory;
    }

    private void Update()
    {
        lastInteractedDebug = lastInteracted;
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            ToggleInventory();
        }
        switch (_state)
        {
            case InventoryState.Items:
                _selector.SetActive(true);
                CheckIfCanNavegateToMenuBar();
                if (Input.GetKeyDown(KeyCode.E)) ShowContextMenu();
                if (Input.GetKeyDown(KeyCode.Escape)) GoToMenuBar();
                break;

            case InventoryState.ContextMenu:
                NavegateVertical();
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Escape)) HideContextMenu();
                break;

            case InventoryState.ItemPickUpPrompt:
                break;

            case InventoryState.NoteViewer:
                if (Input.GetKeyDown(KeyCode.Escape)) CloseNoteViewer();
                Destroy(lastInteracted);
                break;

            case InventoryState.MenuBar:
                stateCooldown += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.DownArrow)) GoToItems();
                if (Input.GetKeyDown(KeyCode.Escape) && stateCooldown > 0.25f) ExitFromMenuBar();
                break;

            case InventoryState.MenuClosed:
                break;

            default:
                break;
        }
    }

    public void SetState(InventoryState state)
    {
        _state = state;
    }

    private void CloseNoteViewer()
    {
        _noteViewer.SetActive(false);
        if (_inventoryViewObject.activeInHierarchy)
        {
            _state = InventoryState.MenuBar;
        }
        else
        {
            _state = InventoryState.MenuClosed;
        }
        Time.timeScale = 1;
    }

    private void ToggleInventory()
    {
        if (_state == InventoryState.MenuClosed)
        {
            _screenFader.FadeToBlack(0.25f, () =>
            {
                EventBus.Instance.OpenInventory();
                _state = InventoryState.Items;
            });
        }
        else if (_state == InventoryState.Items)
        {
            _screenFader.FadeToBlack(0.25f, () =>
            {
                EventBus.Instance.CloseInventory();
            });
        }
    }

    private void OnItemPickedUp(ItemData itemData)
    {
        EventBus.Instance.OpenInventory();
        _promptPanel.SetActive(true);
        _state = InventoryState.ItemPickUpPrompt;
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
        _state = InventoryState.MenuClosed;
    }

    private void ExitFromMenuBar()
    {
        stateCooldown = 0;
        _screenFader.FadeToBlack(0.25f, () =>
        {
            EventBus.Instance.CloseInventory();
        });
    }

    private void GoToItems()
    {
        stateCooldown = 0;
        EventSystem.current.SetSelectedGameObject(_defaultSlot);
        _selector.SetActive(true);
        _state = InventoryState.Items;
    }

    private void HideContextMenu()
    {
        _menuContextInteractableBtns.Clear();
        EventSystem.current.SetSelectedGameObject(_selectedSlot.gameObject);
        _contextMenuObject.SetActive(false);
        _state = InventoryState.Items;
    }

    private void NavegateVertical()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            for (int i = 0; i < _menuContextInteractableBtns.Count; i++)
            {
                if (EventSystem.current.currentSelectedGameObject == _menuContextInteractableBtns[i].gameObject)
                {
                    Debug.Log("Siguiente Seleccionado");
                    if (i + 1 < _menuContextInteractableBtns.Count) EventSystem.current.SetSelectedGameObject(_menuContextInteractableBtns[i + 1].gameObject);
                    if (i + 1 >= _menuContextInteractableBtns.Count) EventSystem.current.SetSelectedGameObject(_menuContextInteractableBtns[0].gameObject);
                    break;
                }
                Debug.Log(EventSystem.current.currentSelectedGameObject + "no es el mismo que" + _menuContextInteractableBtns[i]);
            }
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            for (int i = 0; i < _menuContextInteractableBtns.Count; i++)
            {
                if (EventSystem.current.currentSelectedGameObject == _menuContextInteractableBtns[i].gameObject)
                {
                    if (i - 1 >= 0) EventSystem.current.SetSelectedGameObject(_menuContextInteractableBtns[i - 1].gameObject);
                    if (i - 1 < 0) EventSystem.current.SetSelectedGameObject(_menuContextInteractableBtns[_menuContextInteractableBtns.Count - 1].gameObject);
                    break;
                }
            }
        }
    }

    private void GoToMenuBar()
    {
        stateCooldown = 0;
        EventSystem.current.SetSelectedGameObject(_firstInventoryOption);
        _selector.SetActive(false);
        _state = InventoryState.MenuBar;
    }

    private void CheckIfCanNavegateToMenuBar()
    {
        if (EventSystem.current.currentSelectedGameObject == _firstRowSlots[0] || EventSystem.current.currentSelectedGameObject == _firstRowSlots[1])
        {
            stateCooldown += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.UpArrow) && stateCooldown > 0.25f)
            {
                stateCooldown = 0;
                EventSystem.current.SetSelectedGameObject(_firstInventoryOption);
                _selector.SetActive(false);
                _state = InventoryState.MenuBar;
            }
        }
        else
        {
            stateCooldown = 0;
        }
    }

    private void ShowContextMenu()
    {
        if (EventSystem.current.currentSelectedGameObject.GetComponent<ItemSlot>().itemData != null)
        {
            _state = InventoryState.ContextMenu;
            _contextMenuObject.gameObject.SetActive(true);
            _selector.SetActive(false);
            _equipBtn.interactable = _selectedSlot.itemData.CanBeEquip;
            _checkBtn.interactable = _selectedSlot.itemData.CanBeChecked;
            _useBtn.interactable = _selectedSlot.itemData.CanBeUsed;
            _combineBtn.interactable = _selectedSlot.itemData.CanBeCombined;

            for (int i = 0; i < _menuContextBtns.Count; i++)
            {
                if (_menuContextBtns[i].interactable)
                {
                    _menuContextInteractableBtns.Add(_menuContextBtns[i]);
                }
            }
            _firstInteractable = _menuContextInteractableBtns[0];
            EventSystem.current.SetSelectedGameObject(_firstInteractable.gameObject);
        }
    }

    public void OpenInventory()
    {
        _inventoryViewObject.gameObject.SetActive(true);
        ReOrderSlots();
        _screenFader.FadeFromBlack(0.25f, null);
        SetTextByFirstSlot();
        EventSystem.current.SetSelectedGameObject(_defaultSlot);
        _selector.SetActive(true);
        //_state = State.Items;
    }

    private void SetTextByFirstSlot()
    {
        if (_slots[0].itemData != null)
        {
            _itemNameText.SetText(_slots[0].itemData.Name);
            _itemDescriptionText.SetText(_slots[0].itemData.Descripton[0]);
        }
        else
        {
            _itemNameText.SetText(string.Empty);
            _itemDescriptionText.SetText(string.Empty);
        }
    }

    private void ReOrderSlots()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            Debug.Log("Chequeando slot" + i + 1);
            if (_slots[i].itemData != null)
            {
                _slots[i].DrawSprite();
                continue;
            }
            Debug.Log("Slot" + i + 1 + "es vacio");
            if (i + 1 < _slots.Count)
            {
                for (int j = i + 1; j < _slots.Count; j++)
                {
                    if (_slots[j].itemData != null)
                    {
                        _slots[i].itemData = _slots[j].itemData;
                        _slots[i].stack = _slots[j].stack;
                        _slots[i].DrawSprite();
                        _slots[j].itemData = null;
                        break;
                    }
                }
            }
        }
    }

    public void CloseInventory()
    {
        _inventoryViewObject.gameObject.SetActive(false);
        _screenFader.FadeFromBlack(0.25f, null);
        _state = InventoryState.MenuClosed;
    }
}