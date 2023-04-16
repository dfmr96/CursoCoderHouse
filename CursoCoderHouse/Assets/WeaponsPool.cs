using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsPool : MonoBehaviour
{
    [SerializeField] GameObject[] weapons;
    [SerializeField] GameObject currentWeapon;

    private void OnEnable()
    {
        EventBus.Instance.OnWeaponEquipped += EquipWeapon;
    }

    private void OnDisable()
    {
        EventBus.Instance.OnWeaponEquipped -= EquipWeapon;
    }

    public void EquipWeapon(int weaponId)
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
        weapons[weaponId].SetActive(true);
    }
}
