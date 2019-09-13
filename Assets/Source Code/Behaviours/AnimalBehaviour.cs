﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehaviour : ActorObject
{
    [SerializeField]
    private AnimalType animalType;
    public AnimalType AnimalType { get { return animalType; } private set { animalType = value; } }
    [SerializeField]
    private float playerDetectRange = 5f;
    [SerializeField]
    private float escapeGap = 10f;
    [SerializeField]
    private bool isAlerted;
    private Vector3 movementVector;
    private GameObject playerRef;

    protected override void Start()
    {
        base.Start();
        playerRef = GameMaster.instance.PlayerRef;
    }

    private void Update()
    {
        isAlerted = AlertState(playerRef);
        if (isAlerted)
        {
            Movement();
        }
    }

    private void Movement()
    {
        movementVector = new Vector3(0, 0, movementSpeed) * Time.deltaTime;
        actorTransform.Translate(movementVector);
    }

    private bool AlertState(GameObject target)
    {
        float alertedArea = Vector3.Distance(actorTransform.position, target.transform.position);
        //Initial detect
        if (alertedArea < playerDetectRange)
        {
            return true;
        }
        //Running away
        else if (alertedArea < (playerDetectRange + escapeGap) && isAlerted)
        {
            return true;
        }
        //Back to calm state
        else
        {
            return false;
        }
    }
}