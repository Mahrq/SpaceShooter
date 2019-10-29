using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehaviour : ActorObject
{
    private Animator animalAnim;
    [SerializeField]
    private AnimalType thisAnimalType;
    public AnimalType ThisAnimalType { get { return thisAnimalType; } private set { thisAnimalType = value; } }
    private ObjectPool selectedCorpsePool;
    [SerializeField]
    private float playerDetectRange = 5f;
    [SerializeField]
    private float escapeGap = 10f;
    [SerializeField]
    private bool isAlerted;
    private Vector3 movementVector;
    private GameObject playerRef;
    private GameEventsHandler gameEventsHandler = new GameEventsHandler();
    private bool runningAwayFromGunshot = false;
    private Vector3 startingPositionWhenBulletHeard;
    protected override void Start()
    {
        base.Start();
        playerRef = GameMaster.instance.PlayerRef;
        selectedCorpsePool = GameMaster.instance.PickUpPools[(int)thisAnimalType];
        animalAnim = this.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        CurrentHealth = health;
        GameEventsHandler.OnPlayerShootsWeapon += HeardGunShot;
    }

    private void OnDisable()
    {
        GameEventsHandler.OnPlayerShootsWeapon -= HeardGunShot;
    }

    private void Update()
    {
        isAlerted = AlertState(playerRef);
        animalAnim.SetBool("IsRunningAway", isAlerted);
        if (isAlerted)
        {
            Movement();
        }
        else if (runningAwayFromGunshot)
        {
            if (AlertState(startingPositionWhenBulletHeard, out runningAwayFromGunshot))
            {
                animalAnim.SetBool("IsRunningAway", runningAwayFromGunshot);
                Movement();
            }
        }
    }

    private void Movement()
    {
        movementVector = new Vector3(0, 0, movementSpeed) * Time.deltaTime;
        actorTransform.Translate(movementVector);
    }

    private bool AlertState(Vector3 startPosition, out bool condition)
    {
        float runningAwayArea = Vector3.Distance(startPosition, actorTransform.position);
        if (runningAwayArea < escapeGap)
        {
            condition = true;
            return true;
        }
        else
        {
            condition = false;
            return false;
        }

    }
    private bool AlertState(GameObject target)
    {
        float alertedArea = Vector3.Distance(actorTransform.position, target.transform.position);
        //Debug.Log("Distance from animal: " + alertedArea.ToString("F2"));
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

    protected override void Death(bool deathStatus)
    {
        if (deathStatus)
        {
            selectedCorpsePool.SpawnObject(actorTransform.position, Quaternion.identity);
            gameEventsHandler.CallEvent(thisAnimalType);
            actorGameObject.SetActive(false);
        }
    }

    private void HeardGunShot(Vector3 eventParam)
    {
        float distanceFromPlayer = Vector3.Distance(actorTransform.position, playerRef.transform.position);
        if (distanceFromPlayer < playerDetectRange + escapeGap)
        {
            startingPositionWhenBulletHeard = actorTransform.position;
            runningAwayFromGunshot = true;
        }
    }
}
