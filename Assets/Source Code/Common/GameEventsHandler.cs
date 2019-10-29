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
    public delegate void IntParams(int intNumber);
    public delegate void AnimalParams(AnimalType animalType);

    //Player Events
    public static event IntParams OnPlayerUpdateHealth;
    public static event Vector3Params OnPlayerShootsWeapon;
    public static event GunTypeParams OnPlayerSwapWeapon;
    public static event NoParams OnPlayerDeath;

    //Game Events
    public static event AnimalParams OnAnimalDeath;

    public void CallEvent(PlayerEvent playerEvent)
    {
        switch (playerEvent)
        {
            case PlayerEvent.Death:
                CheckEventSubscribers(OnPlayerDeath);
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

    public void CallEvent(PlayerEvent playerEvent, int intNumber)
    {
        switch (playerEvent)
        {
            case PlayerEvent.HealthUpdate:
                CheckEventSubscribers(OnPlayerUpdateHealth, intNumber);
                break;
            default:
                Debug.LogError("Invalid Parameter for this overload of the method");
                break;
        }
    }

    public void CallEvent(AnimalType animalType)
    {
        CheckEventSubscribers(OnAnimalDeath, animalType);
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

    private void CheckEventSubscribers(IntParams eventToCheck, int intNumber)
    {
        if (eventToCheck != null)
        {
            eventToCheck(intNumber);
        }
    }

    private void CheckEventSubscribers(AnimalParams eventToCheck, AnimalType animalType)
    {
        if (eventToCheck != null)
        {
            eventToCheck(animalType);
        }
    }

}
