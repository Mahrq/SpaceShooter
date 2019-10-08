using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUIBehaviour : MonoBehaviour
{
    #region Assign in inspector
    [SerializeField]
    private GameObject selector;
    [SerializeField]
    private RectTransform[] weaponIcons;
    [SerializeField]
    private Text[] ammoCountText;
    [SerializeField]
    private GameObject[] healthIcons;
    #endregion


    private void OnEnable()
    {
        GameEventsHandler.OnPlayerSwapWeapon += UpdateSelectedWeaponIcon;
        GameEventsHandler.OnPlayerShootsWeapon += UpdateAmmoCountText;
        GameEventsHandler.OnPlayerUpdateHealth += UpdateHealthIcons;
    }

    private void OnDisable()
    {
        GameEventsHandler.OnPlayerSwapWeapon -= UpdateSelectedWeaponIcon;
        GameEventsHandler.OnPlayerShootsWeapon -= UpdateAmmoCountText;
        GameEventsHandler.OnPlayerUpdateHealth -= UpdateHealthIcons;
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

    private void UpdateHealthIcons(int playerHealth)
    {
        bool showIcon;
        for (int i = 0; i < healthIcons.Length; i++)
        {
            //Health used to help index health icons
            showIcon = i <= (playerHealth - 1);
            healthIcons[i].SetActive(showIcon);
        }
    }
}
