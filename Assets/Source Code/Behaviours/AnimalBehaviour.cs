using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehaviour : ActorObject
{
    private Animator animalAnim;
    [SerializeField]
    private AnimalType animalType;
    public AnimalType AnimalType { get { return animalType; } private set { animalType = value; } }
    private ObjectPool selectedCorpsePool;
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
        selectedCorpsePool = GameMaster.instance.PickUpPools[(int)animalType];
        animalAnim = this.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        CurrentHealth = health;
    }

    private void Update()
    {
        isAlerted = AlertState(playerRef);
        animalAnim.SetBool("IsRunningAway", isAlerted);
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
            actorGameObject.SetActive(false);
        }
    }
}
