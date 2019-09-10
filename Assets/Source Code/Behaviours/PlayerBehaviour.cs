using UnityEngine;
using System.Collections;

public class PlayerBehaviour : ActorObject
{
    [SerializeField]
    private Transform gunBarrelTransform;
    private Vector3 bulletSpawnPosition;
    [SerializeField]
    private GunType selectedWeapon;
    private ObjectPool selectedBulletPool;
    private Vector3 movementVector;
    private float hInput, vInput;


    protected override void Start()
    {
        base.Start();
        bulletSpawnPosition = gunBarrelTransform.position;
        selectedBulletPool = GameMaster.instance.BulletPools[(int)selectedWeapon];
    }
    private void Update()
    {

        Movement(actorTransform);
        
    }


    private void Movement(Transform objectToMove)
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
        movementVector = new Vector3(hInput, 0, vInput) * movementSpeed * Time.deltaTime;
        objectToMove.Translate(movementVector);
    }

    private void Shoot()
    {

    }

}
