using UnityEngine;
using System.Collections;
/// <summary>
/// Script controls the player's movement and shooting capability
/// </summary>
public class PlayerBehaviour : ActorObject
{
    [SerializeField]
    private Transform gunBarrelTransform;
    [SerializeField]
    private GunType selectedWeapon;
    private ObjectPool selectedBulletPool;
    private Vector3 movementVector;
    private float hInput, vInput;


    protected override void Start()
    {
        base.Start();
        selectedBulletPool = GameMaster.instance.BulletPools[(int)selectedWeapon];
    }
    private void Update()
    {

        Movement(actorTransform);
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            CycleWeapon(ref selectedWeapon);
        }
        
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
        selectedBulletPool.SpawnObject(gunBarrelTransform.position, Quaternion.identity);
    }

    private void CycleWeapon(ref GunType currentWeapon)
    {
        int current = (int)currentWeapon;
        current++;
        current %= GameMaster.instance.BulletPools.Length;
        currentWeapon = (GunType)current;
        selectedBulletPool = GameMaster.instance.BulletPools[(int)currentWeapon];
        Debug.LogFormat("WeaponSwitch: {0}", selectedBulletPool.ObjectPoolName);
    }

}
