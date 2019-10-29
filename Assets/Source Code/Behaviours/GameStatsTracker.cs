using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameStatsTracker : MonoBehaviour
{
    [SerializeField] // set in inspector
    private GameObject StatsOverLay;
    [SerializeField] // set in inspector
    private Text[] trackedStatsUI;
    private int[] trackedStats = new int[4];

    private enum GameStats
    {
        RabbitsKilled,
        DeersKilled,
        BearsKilled,
        DistanceTraveled,
    }

    private void OnEnable()
    {
        GameEventsHandler.OnAnimalDeath += UpdateScore;
        GameEventsHandler.OnPlayerDeath += AssignStats;
    }

    private void OnDisable()
    {
        GameEventsHandler.OnAnimalDeath -= UpdateScore;
        GameEventsHandler.OnPlayerDeath -= AssignStats;
    }

    private void Start()
    {
        StatsOverLay.SetActive(false);
    }
    
    private void AssignStats()
    {
        StatsOverLay.SetActive(true);
        trackedStatsUI[(int)GameStats.RabbitsKilled].text = trackedStats[(int)GameStats.RabbitsKilled].ToString();
        trackedStatsUI[(int)GameStats.DeersKilled].text = trackedStats[(int)GameStats.DeersKilled].ToString();
        trackedStatsUI[(int)GameStats.BearsKilled].text = trackedStats[(int)GameStats.BearsKilled].ToString();
        trackedStatsUI[(int)GameStats.DistanceTraveled].text = string.Format($"{GameMaster.instance.PlayerRef.transform.position.z.ToString("F0")} meters");
    }

    private void UpdateScore(AnimalType animalType)
    {
        trackedStats[(int)animalType]++;
    }



}
