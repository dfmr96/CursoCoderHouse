using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Note Item Data", menuName = "Inventory/New Note Item Data")]

public class NoteItemData : ItemData
{
    public string Header => _header;
    public string Pages => _textField;
    public Sprite Background => _background;

    public string _header;
    [Multiline(5)] public string _textField;
    [SerializeField] private Sprite _background;
    public NoteItemData()
    {
        base._itemType = ItemType.note;
    }
}
