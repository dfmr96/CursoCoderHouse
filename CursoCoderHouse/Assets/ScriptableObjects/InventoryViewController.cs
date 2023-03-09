using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryViewController : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryViewObject;
    [SerializeField] private TMP_Text _itemNameText;
    [SerializeField] private TMP_Text _itemDescriptionText;

    [SerializeField] private PlayerController _playerController;

    [SerializeField] private List<ItemSlot> _slots;

    [SerializeField] private ScreenFader _screenFader;

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

        _itemNameText.SetText(selectedSlot.itemData.Name);
        _itemDescriptionText.SetText(selectedSlot.itemData.Descripton[0]);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {

            _screenFader.FadeToBlack(0.25f, () =>
            {
                if (_inventoryViewObject.activeSelf)
                {
                    Debug.Log("true");
                    EventBus.Instance.CloseInventory();
                }
                else
                {
                    EventBus.Instance.OpenInventory();
                }
                _inventoryViewObject.SetActive(!_inventoryViewObject.activeSelf);
                _screenFader.FadeFromBlack(0.25f, null);
            });
        }
    }
}
