using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    [SerializeField]
    private GameObject playerRef;
    public GameObject PlayerRef { get { return playerRef; } private set { playerRef = value; } }
    [SerializeField]
    private GameObject bulletPoolHolder;
    public ObjectPool[] BulletPools { get; private set; }
    [SerializeField]
    private GameObject pickUpPoolHolder;
    public ObjectPool[] PickUpPools { get; private set; }
    [SerializeField]
    private GameObject tilePoolHolder;
    public ObjectPool TilePool { get; private set; }
    [SerializeField]
    private GameObject animalPoolHolder;
    public ObjectPool[] AnimalPools { get; private set; }

    private ObjectPool bloodSplatFx;
    public ObjectPool BloodSplatFx
    {
        get
        {
            if (bloodSplatFx == null)
            {
                bloodSplatFx = this.GetComponent<ObjectPool>();
            }
            return bloodSplatFx;
        }
    }

    [SerializeField]
    private int gameTileCount = 2;
    public int GameTileCount { get { return gameTileCount; } }

    [SerializeField]
    private int tileLength = 32; //Physical length of the tile
    public int TileLength { get { return tileLength; } }
    private void Awake()
    {
        instance = this;

        //Set references to the object pool. Pools in arrays are ordered by their ID set in the inspector.
        bloodSplatFx = this.GetComponent<ObjectPool>();
        BulletPools = bulletPoolHolder.GetComponents<ObjectPool>().OrderBy(id => id.ObjectPoolID).ToArray();
        PickUpPools = pickUpPoolHolder.GetComponents<ObjectPool>().OrderBy(id => id.ObjectPoolID).ToArray();
        AnimalPools = animalPoolHolder.GetComponents<ObjectPool>().OrderBy(id => id.ObjectPoolID).ToArray();
        TilePool = tilePoolHolder.GetComponent<ObjectPool>();
    }

    private void Start()
    {
        SpawnStartingLevel(gameTileCount);
    }

    private void SpawnStartingLevel(int tileCount)
    {
        Vector3 spawnPosition;
        int zPos = 32; // Start spawning the next tile after the first tile that is manually placed;
        for (int i = 1; i < tileCount + 1; i++)
        {
            zPos = i * tileLength;
            spawnPosition = new Vector3(0, 0, zPos);
            TilePool.SpawnObject(spawnPosition, Quaternion.identity);
        }
    }
}
