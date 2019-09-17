using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponGUIBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject selector;
    [SerializeField]
    private RectTransform[] weaponIcons;
    [SerializeField]
    private Text[] ammoCountText;

    private void OnEnable()
    {
        GameEventsHandler.OnPlayerSwapWeapon += UpdateSelectedWeaponIcon;
        GameEventsHandler.OnPlayerShootsWeapon += UpdateAmmoCountText;
    }

    private void OnDisable()
    {
        GameEventsHandler.OnPlayerSwapWeapon -= UpdateSelectedWeaponIcon;
        GameEventsHandler.OnPlayerShootsWeapon -= UpdateAmmoCountText;
    }

    private void UpdateSelectedWeaponIcon(GunType gunType)
    {
        selector.transform.SetParent(weaponIcons[(int)gunType]);
        selector.transform.localPosition = Vector2.zero;
    }

    private void UpdateAmmoCountText(Vector3 ammoCount)
    {
        ammoCountText[(int)GunType.Rifle].text = ammoCount.x.ToString("F0");
        ammoCountText[(int)GunType.Shotgun].text = ammoCount.y.ToString("F0");
        ammoCountText[(int)GunType.Scoped].text = ammoCount.z.ToString("F0");
    }
}
