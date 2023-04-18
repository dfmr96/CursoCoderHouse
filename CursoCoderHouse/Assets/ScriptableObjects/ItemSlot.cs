using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, ISelectHandler
{
    public ItemData itemData;
    private InventoryViewController _viewController;
    private Image _spawnedSprite;
    public int stack;
    public int maxStack;
    public TMP_Text stackText;

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
        //if (itemData == null) return;
    }

    public void DrawSprite()
    {
        stackText.SetText(stack.ToString());
        stackText.color = itemData.GetColor();

        if (itemData.GetColor() == Color.red) stackText.color = Color.green;
        if (itemData is KeyItemData) stackText.SetText(string.Empty);
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
