using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalLootBehaviour : MonoBehaviour
{
    [SerializeField]
    private int healAmount;
    [SerializeField]
    [Range(0f, 1f)]
    private float chanceOfAmmoReplenish;

    private void OnTriggerEnter(Collider other)
    {
        PlayerBehaviour playerContact = other.GetComponent<PlayerBehaviour>();
        if (playerContact)
        {
            float chanceRoll = Random.value;
            if (chanceRoll < chanceOfAmmoReplenish)
            {
                playerContact.ReplenishAmmo();
            }
            playerContact.TakeDamage(-healAmount);
            Hide();
        }
    }

    private void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
