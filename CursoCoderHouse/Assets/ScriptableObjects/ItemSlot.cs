using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, ISelectHandler
{
    public ItemData itemData;
    private InventoryViewController _viewController;
    private Image _spawnedSprite;

    public void OnSelect(BaseEventData eventData)
    {
        _viewController.OnSlotSelected(this);
    }

    public bool isEmpty()
    {
        return itemData == null;
    }

    private void OnEnable()
    {
        _viewController = FindObjectOfType<InventoryViewController>();
        if (itemData == null) return;
        _spawnedSprite = Instantiate<Image>(itemData.Sprite, transform.position, Quaternion.identity, transform);
    }

    private void OnDisable()
    {
        if (_spawnedSprite != null)
        {
            Destroy( _spawnedSprite);
        }
    }
}
