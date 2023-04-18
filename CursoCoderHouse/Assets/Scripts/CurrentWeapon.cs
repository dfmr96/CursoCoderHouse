using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWeapon : MonoBehaviour
{
    public static CurrentWeapon Instance;

    public WeaponItemData weaponItemData;
    public int stack;
    public int maxStack;
    public Button weaponBtn;
    public TMP_Text stackText;

    private void Awake()
    {
        if (Instance == null) 
        { 
            Instance = this; 
        } else
        {
            Destroy(Instance);
        }
    }

    private void OnEnable()
    {
        if (weaponItemData != null)
        {
            stackText.SetText(stack.ToString());
        }
    }
}
