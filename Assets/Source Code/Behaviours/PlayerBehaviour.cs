using UnityEngine;
using System.Collections;
/// <summary>
/// Script controls the player's movement and shooting capability
/// </summary>
public class PlayerBehaviour : ActorObject
{
    //References
    [SerializeField]
    private Transform gunBarrelTransform;
    [SerializeField]
    private GunType selectedWeapon;
    private ObjectPool selectedBulletPool;
    private GameEventsHandler gameEventsHandler = new GameEventsHandler();
    //Weapon properties
    [SerializeField]
    [Tooltip("x: Rifle\ny: Shotgun\nz:Scoped Rifle")]
    private Vector3 weaponFireRates;
    [SerializeField]
    [Tooltip("x: Rifle\ny: Shotgun\nz:Scoped Rifle")]
    private Vector3 weaponAmmoCount;
    private int currentFireRate;
    private bool canShoot = true;
    private int frameCounter = 0;
    //Movement properties
    private Vector3 movementVector;
    private float hInput, vInput;
    
    protected override void Start()
    {
        base.Start();
        selectedWeapon = GunType.Rifle;
        selectedBulletPool = GameMaster.instance.BulletPools[(int)selectedWeapon];
        currentFireRate = (int)weaponFireRates.x;
        gameEventsHandler.CallEvent(PlayerEvent.SwapWeapon, selectedWeapon);
        gameEventsHandler.CallEvent(PlayerEvent.ShootWeapon, weaponAmmoCount);
    }
    private void Update()
    {
        

        Movement(actorTransform);
        //Start the counter when the player is unable to shoot to simulate fire rate.
        if (!canShoot)
        {
            frameCounter++;
            //Allow player to be able to shoot when the counter reaches the rate of fire.
            if (frameCounter % currentFireRate == 0)
            {
                canShoot = true;
            }
        }
        //Resets the bool canShoot to false and frame counter to 0 on left click.
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            Shoot(selectedWeapon, ref weaponAmmoCount);
            canShoot = false;
            frameCounter = 0;
        }
        //Cycle the selected weapon on right click, allow switching only if player can shoot.
        if (Input.GetButtonDown("Fire2") && canShoot)
        {
            CycleWeapon(ref selectedWeapon);
        }
        
    }
    //Movement controls with left and right input.
    private void Movement(Transform objectToMove)
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
        movementVector = new Vector3(hInput, 0, vInput) * movementSpeed * Time.deltaTime;
        objectToMove.Translate(movementVector);
    }
    //Depending on the slected weapon, the method will spawn the type of bullet and subtract the ammo amount.
    private void Shoot(GunType currentWeapon, ref Vector3 currentAmmoSet)
    {
        switch (currentWeapon)
        {
            case GunType.Rifle:
                if (currentAmmoSet.x > 0)
                {
                    selectedBulletPool.SpawnObject(gunBarrelTransform.position, Quaternion.identity);
                    currentAmmoSet.x--;
                }
                break;
            case GunType.Shotgun:
                if (currentAmmoSet.y > 0)
                {
                    selectedBulletPool.SpawnObject(gunBarrelTransform.position, Quaternion.identity);
                    currentAmmoSet.y--;
                }
                break;
            case GunType.Scoped:
                if (currentAmmoSet.z > 0)
                {
                    selectedBulletPool.SpawnObject(gunBarrelTransform.position, Quaternion.identity);
                    currentAmmoSet.z--;
                }
                break;
            default:
                if (currentAmmoSet.x > 0)
                {
                    selectedBulletPool.SpawnObject(gunBarrelTransform.position, Quaternion.identity);
                    currentAmmoSet.x--;
                }
                break;
        }
        gameEventsHandler.CallEvent(PlayerEvent.ShootWeapon, currentAmmoSet);
    }
    //Cycle the current weapon onto the next, changing the current fire rate requirements and object pool to draw from.
    private void CycleWeapon(ref GunType currentWeapon)
    {
        int current = (int)currentWeapon;
        current++;
        current %= GameMaster.instance.BulletPools.Length;
        currentWeapon = (GunType)current;
        selectedBulletPool = GameMaster.instance.BulletPools[(int)currentWeapon];
        switch (currentWeapon)
        {
            case GunType.Rifle:
                currentFireRate = (int)weaponFireRates.x;
                break;
            case GunType.Shotgun:
                currentFireRate = (int)weaponFireRates.y;
                break;
            case GunType.Scoped:
                currentFireRate = (int)weaponFireRates.z;
                break;
            default:
                currentFireRate = (int)weaponFireRates.x;
                break;
        }
        gameEventsHandler.CallEvent(PlayerEvent.SwapWeapon, currentWeapon);
        Debug.LogFormat("WeaponSwitch: {0}", selectedBulletPool.ObjectPoolName);
    }
}
