using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Description:    Handles the trigger behaviour when the player passess the Trigger.
///                 The behaviour will spawn the next tile
///                 Game Objects with this behaviour should have the GameEventTrigger Layer which allows
///                 collision with Game Objects with the Player Layer.
/// </summary>
public class LevelTileBehaviour : MonoBehaviour
{
    private Transform myTransform;
    private ObjectPool tilePool;
    private ObjectPool[] animalPools;
    [SerializeField]
    private bool canSpawnAnimals = true;
    private int spawnedAnimalsCounter = 0;

    private void Start()
    {
        myTransform = this.GetComponent<Transform>();
        tilePool = GameMaster.instance.TilePool;
        animalPools = GameMaster.instance.AnimalPools;
        if (canSpawnAnimals)
        {
            SpawnAnimal();
        }
    }

    private void OnEnable()
    {
        if (myTransform == null)
        {
            myTransform = this.GetComponent<Transform>();
        }
        if (myTransform)
        {
            if (canSpawnAnimals && spawnedAnimalsCounter > 0)
            {
                SpawnAnimal();
            }
        }
    }

    /// <summary>
    /// When the player passes the trigger point, it will spawn the next tile and desapwn
    /// the current tile.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        SpawnNextTile();
    }

    private void SpawnNextTile()
    {
        int zPos = (int)myTransform.position.z + (GameMaster.instance.GameTileCount * GameMaster.instance.TileLength);
        Vector3 spawnPosition = new Vector3(0, 0, zPos);
        tilePool.SpawnObject(spawnPosition, Quaternion.identity);
        this.gameObject.SetActive(false);
    }
    /// <summary>
    /// Spawns an animal anywhere within the dimensions of the tile.
    /// </summary>
    private void SpawnAnimal()
    {
        int xPos = Random.Range(-7, 8);
        int zPos = (int)Random.Range((myTransform.position.z - 16),
                                (myTransform.position.z + 16));
        int chosenAnimal = Random.Range((int)AnimalType.Rabbit, (int)AnimalType.Bear + 1);
        Vector3 spawnPosition = new Vector3(xPos, 0, zPos);
        animalPools[chosenAnimal].SpawnObject(spawnPosition, Quaternion.identity);
        spawnedAnimalsCounter++;
    }
}
