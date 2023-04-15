using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Note Item Data", menuName = "Inventory/New Note Item Data")]

public class NoteItemData : ItemData
{
    public string Header => _header;
    public string[] Pages => _pages;
    public Sprite Background => _background;

    [SerializeField] private string _header;
    [SerializeField] private string[] _pages;
    [SerializeField] private Sprite _background;
    private void Awake()
    {
        base._itemType = ItemType.Note;
        base.SetColor();
    }
}
