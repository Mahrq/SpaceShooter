using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class Stores all the game events that need to be called.
/// Instantiate class to use the events and methods
/// </summary>
public class GameEventsHandler : MonoBehaviour
{
    public delegate void NoParams();

    //Player Events
    public static event NoParams OnPlayerUpdateHealth;
    public static event NoParams OnPlayerShootsWeapon;
    public static event NoParams OnPlayerSwapWeapon;
    public static event NoParams OnPlayerDeath;


    private void CheckEventSubscribers(NoParams eventToCheck)
    {
        if (eventToCheck != null)
        {
            eventToCheck();
        }
    }

}
