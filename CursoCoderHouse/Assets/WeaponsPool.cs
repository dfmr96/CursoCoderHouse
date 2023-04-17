using UnityEngine;

public class WeaponsPool : MonoBehaviour
{
    public static WeaponsPool instance;
    [SerializeField] GameObject[] weapons;
    public GameObject currentWeapon;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    private void OnEnable()
    {
        EventBus.Instance.OnWeaponEquipped += EquipWeapon;
    }

    private void OnDisable()
    {
        EventBus.Instance.OnWeaponEquipped -= EquipWeapon;
    }

    public Vector3 GetWeaponOrigin()
    {
        return currentWeapon.GetComponent<Weapon>().bulletOrigin.position;
    }

    public void EquipWeapon(int weaponId)
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
        weapons[weaponId].SetActive(true);
        currentWeapon = weapons[weaponId];
    }
}
