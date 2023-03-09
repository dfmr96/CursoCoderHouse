using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public GameObject inventorySlotPrefab;
    //public InventoryObject inventory;
    //Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    private void Start()
    {
        //  CreateSlots();
    }

    private void Update()
    {
        UpdateSlots();
    }

    public void UpdateSlots()
    {
        /*   foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
           {
               if (_slot.Value.ID >= 0)
               {
                   _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.ID].uiDisplay;
                   _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                   _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
               }
               else
               {
                   _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                   _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                   _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
               }
           }
       }
       public void CreateSlots()
       {
           itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

           for (int i = 0; i < inventory.Container.Items.Length; i++)
           {
               var obj = Instantiate(inventorySlotPrefab, Vector3.zero, Quaternion.identity, transform);
               itemsDisplayed.Add(obj, inventory.Container.Items[i]);
           }
       }
        */
    }
}
