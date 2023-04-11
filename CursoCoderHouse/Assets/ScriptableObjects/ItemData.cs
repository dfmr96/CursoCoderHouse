using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ItemType
{
    Key,
    Medical,
    Note,
    Ammo,
    Weapon
}
[CreateAssetMenu(fileName = "Default Item Data", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    public string Name => _name;
    public List<string> Descripton => _description;
    public Image Sprite => _sprite;

    public bool CanBeUsed => _canUse;
    public bool CanBeChecked => _canCheck;
    public bool CanBeCombined => _canCombine;
    public bool CanBeEquip => _canEquip;

    [SerializeField] private string _name;
    [SerializeField] private List<string> _description;
    [SerializeField] private Image _sprite;
    [SerializeField] private bool _canUse;
    [SerializeField] private bool _canEquip;
    [SerializeField] private bool _canCheck;
    [SerializeField] private bool _canCombine;
    [SerializeField] protected ItemType _itemType;
    public Color itemTypeColor;

    private Color _keyColor = Color.yellow;
    private Color _medicalColor = Color.green;
    private Color _noteColor = Color.white;
    private Color _weaponColor = Color.red;
    private Color _ammoColor = Color.blue;

    protected virtual void SetColor()
    {
        switch (_itemType)
        {
            case ItemType.Key:
                itemTypeColor = _keyColor; 
                break;
            case ItemType.Medical:
                itemTypeColor = _medicalColor;
                break;
            case ItemType.Note:
                itemTypeColor = _noteColor;
                break;
            case ItemType.Weapon:
                itemTypeColor = _weaponColor; 
                break;
            case ItemType.Ammo:
                itemTypeColor = _ammoColor; 
                break;
        }
    }
}
