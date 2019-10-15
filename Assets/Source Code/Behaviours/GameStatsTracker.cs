using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatsTracker : MonoBehaviour
{
    [SerializeField]
    private Text[] trackedStatsUI;
    private int[] trackedStats;
    private float gameSessionTime;

    private enum GameStats
    {
        DistanceTraveled,
        RabbitsKilled,
        DeersKilled,
        BearsKille,
        SessionTime
    }

    private void Start()
    {
        gameSessionTime = 0;
    }

    private void Update()
    {
        gameSessionTime += Time.deltaTime;
    }

}
