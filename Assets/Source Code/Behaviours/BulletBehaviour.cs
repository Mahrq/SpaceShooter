using UnityEngine;
using System.Collections;
/// <summary>
/// Bullet properties for each bullet type
/// Attach script to the different bullets and assign their properties.
/// </summary>
public class BulletBehaviour : ActorObject
{
    //Properties, set in the inspector
    [SerializeField]
    private GunType thisGunType;
    [SerializeField]
    private AnimalType strongAgainst;
    [SerializeField]
    private AnimalType weakAgainst;
    [SerializeField]
    private float maxRange = 10f;
    [SerializeField]
    private int damageBase = 2;
    [SerializeField]
    private float speedMultiplier = 2f;
    private Vector3 movementVector;
    private Vector3 startingPosition;

    protected override void Start()
    {
        base.Start();
        startingPosition = actorTransform.position;
    }

    private void OnEnable()
    {
        if (actorTransform)
        {
            if (startingPosition == Vector3.zero)
            {
                startingPosition = actorTransform.position;
            }
        }
  
    }

    private void OnDisable()
    {
        startingPosition = Vector3.zero;
    }

    private void Update()
    {
        Movement(maxRange);
    }

    private void Movement(float range)
    {
        //If bullet exceeds its travel distance allowed then object is disabled.
        if (Vector3.Distance(startingPosition, actorTransform.position) >= range)
        {
            TakeDamage(CurrentHealth);
        }
        //Move the bullet forward in a straight direction.
        else
        {
            movementVector = new Vector3(0, 0, movementSpeed) * speedMultiplier * Time.deltaTime;
            actorTransform.Translate(movementVector);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AnimalBehaviour animalShot = other.GetComponent<AnimalBehaviour>();
        if (animalShot)
        {
            animalShot.TakeDamage(DamageCalculation(damageBase, animalShot.ThisAnimalType));
            //Destroy bullet on impact aswell
            TakeDamage(CurrentHealth);
        }
    }

    private int DamageCalculation(int baseDamage, AnimalType targetCompare)
    {

        if (targetCompare == strongAgainst)
        {
            return baseDamage * 2;
        }
        else if (targetCompare == weakAgainst)
        {
            return (baseDamage - 1);
        }

        return baseDamage;
    }

}
