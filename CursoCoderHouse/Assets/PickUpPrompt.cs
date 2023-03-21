using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PickUpPrompt : MonoBehaviour
{
    [SerializeField] GameObject promptPanel;
    [SerializeField] TMP_Text promptText;
    [SerializeField] Button yesBtn;
    [SerializeField] Button noBtn;

    private void OnEnable()
    {
        EventBus.Instance.onPickUpPrompt += Prompt; 
    }

    private void OnDisable()
    {
        EventBus.Instance.onPickUpPrompt -= Prompt;
    }

    public void Prompt(ItemData itemData, Action yesAction, Action noAction)
    {
        EventBus.Instance.OpenInventory();
        EventSystem.current.SetSelectedGameObject(yesBtn.gameObject);
        promptText.SetText($"Do you want to take {itemData.Name}?");
        yesBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(yesAction));
        noBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(noAction));
    }
}
