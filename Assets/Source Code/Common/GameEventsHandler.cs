using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class Stores all the game events that need to be called.
/// Instantiate class to use the events and methods
/// </summary>
public class GameEventsHandler
{
    public delegate void NoParams();
    public delegate void Vector3Params(Vector3 vector);
    public delegate void GunTypeParams(GunType gunType);

    //Player Events
    public static event NoParams OnPlayerUpdateHealth;
    public static event Vector3Params OnPlayerShootsWeapon;
    public static event GunTypeParams OnPlayerSwapWeapon;
    public static event NoParams OnPlayerDeath;


    public void CallEvent(PlayerEvent playerEvent)
    {
        switch (playerEvent)
        {
            case PlayerEvent.Death:
                break;
            default:
                Debug.LogError("Invalid Parameter for this overload of the method");
                break;
        }
    }

    public void CallEvent(PlayerEvent playerEvent, GunType gunType)
    {
        switch (playerEvent)
        {
            case PlayerEvent.SwapWeapon:
                CheckEventSubscribers(OnPlayerSwapWeapon, gunType);
                break;
            default:
                Debug.LogError("Invalid Parameter for this overload of the method");
                break;
        }
    }

    public void CallEvent(PlayerEvent playerEvent, Vector3 vector)
    {
        switch (playerEvent)
        {
            case PlayerEvent.ShootWeapon:
                CheckEventSubscribers(OnPlayerShootsWeapon, vector);
                break;
            default:
                Debug.LogError("Invalid Parameter for this overload of the method");
                break;
        }
    }

    private void CheckEventSubscribers(NoParams eventToCheck)
    {
        if (eventToCheck != null)
        {
            eventToCheck();
        }
    }

    private void CheckEventSubscribers(GunTypeParams eventToCheck, GunType gunType)
    {
        if (eventToCheck != null)
        {
            eventToCheck(gunType);
        }
    }

    private void CheckEventSubscribers(Vector3Params eventToCheck, Vector3 vector)
    {
        if (eventToCheck != null)
        {
            eventToCheck(vector);
        }
    }

}
