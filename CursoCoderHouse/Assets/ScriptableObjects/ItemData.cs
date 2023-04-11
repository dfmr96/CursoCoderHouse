using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
}
