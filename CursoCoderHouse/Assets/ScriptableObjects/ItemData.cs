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

    [SerializeField] private string _name;
    [SerializeField] private List<string> _description;
    [SerializeField] private Image _sprite;
}
