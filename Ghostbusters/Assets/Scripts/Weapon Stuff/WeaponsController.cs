using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    //change to subclass weapons/items later
    [SerializeField]
    List<Weapon> weapons = new List<Weapon>();
    Weapon currentWeapon;

    public int CurrentWeaponIndex
    {
        get { return _currentWeaponIndex; }
        set
        {
            _currentWeaponIndex = Mathf.Clamp(value, 0, weapons.Count);

            if(_currentWeaponIndex >= weapons.Count)
            {
                _currentWeaponIndex = 0;
            }
        }
    }

    private int _currentWeaponIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = weapons[CurrentWeaponIndex];
        currentWeapon.gameObject.SetActive(true);
    }

    public void ChangeWeapon()
    {
        //disable current weapon
        weapons[CurrentWeaponIndex].gameObject.SetActive(false);
        //change to and assign new weapon
        CurrentWeaponIndex++;
        currentWeapon = weapons[_currentWeaponIndex];
        //enable new weapon
        weapons[CurrentWeaponIndex].gameObject.SetActive(true);
    }

    public void UseWeapon()
    {
        currentWeapon.Use();
    }
}
