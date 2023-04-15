using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class Note : MonoBehaviour, IInteractable
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private GameObject _noteViewer;
    [SerializeField] private TMP_Text _header;
    [SerializeField] private TMP_Text _pageField;
    [SerializeField] private Button _leftBtn;
    [SerializeField] private Button _rightBtn;
    [SerializeField] private Image _background;
    [SerializeField] private NoteItemData _itemData;

    public void Interact(ItemData item)
    {
        Time.timeScale = 0f;
        _noteViewer.SetActive(true);
        _header.SetText(_itemData.Header);
        _pageField.SetText(_itemData.Pages[0]);
        _background.sprite = _itemData.Background;
    }
}
