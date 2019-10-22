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
    private int[] trackedStats = new int[5];
    private float gameSessionTimer;
    private TimeSpan gameSessionTime = new TimeSpan();

    private enum GameStats
    {
        RabbitsKilled,
        DeersKilled,
        BearsKilled,
        DistanceTraveled,
        SessionTime
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        gameSessionTimer = 0;
    }

    private void Update()
    {
        gameSessionTimer += Time.deltaTime;
        gameSessionTime = TimeSpan.FromSeconds(gameSessionTimer);
        //string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D}");
        //Debug.Log(formattedTime);
    }







}
