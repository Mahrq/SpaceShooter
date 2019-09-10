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


    private void Awake()
    {
        instance = this;
        BulletPools = bulletPoolHolder.GetComponents<ObjectPool>();
        BulletPools = BulletPools.OrderBy(id => id.ObjectPoolID).ToArray();
    }
}
