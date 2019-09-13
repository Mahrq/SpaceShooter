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
    }

    private void OnEnable()
    {
        startingPosition = actorTransform.position;
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
            TakeDamage(Health);
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
            animalShot.TakeDamage(DamageCalculation(damageBase, animalShot.AnimalType));
            //Destroy bullet on impact aswell
            TakeDamage(Health);
        }
    }

    private int DamageCalculation(int baseDamage, AnimalType targetCompare)
    {
        int totalDamage = baseDamage;

        if (targetCompare == strongAgainst)
        {
            return totalDamage = baseDamage * 2;
        }
        else if (targetCompare == weakAgainst)
        {
            return totalDamage = (baseDamage - 1);
        }

        return totalDamage;
    }

}
