using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Mark Mendoza
/// Class that handles spawning and object pooling for one or a collection of objects.
/// Set name, prefab to spawn, choose first and capacity in the inspector.
/// </summary>
public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Name this object pool for searching purposes.")]
    private string objectPoolName;
    public string ObjectPoolName { get { return objectPoolName; } }

    [SerializeField]
    [Tooltip("Put a numeric ID for this pool to make it more compatable with enums or linq sorting")]
    private int objectPoolID;
    public int ObjectPoolID { get { return objectPoolID; } }

    [SerializeField]
    [Tooltip("Spawns chosen prefab and will add to pool when spawned.")]
    private GameObject[] prefabsToSpawnAndPool;

    [SerializeField]
    [Tooltip("When attempting to reuse from pool, chooese whether the first object in the pool is chosen.")]
    private bool chooseFirstObjectInPool = true;

    [SerializeField]
    [Tooltip("Set how many objects the pool can contain.")]
    private int maxCapacity;
    private bool atMaxCapacity;
    public bool AtMaxCapacity { get { return atMaxCapacity; } }
    [SerializeField]
    [Tooltip("Check if you want the pool to grow if there aren't any objects to reuse regardless of pool size")]
    private bool dynamicMode;

    [SerializeField]
    [Tooltip("View currently pooled objects, DO NOT DRAG OBJECTS HERE.")]
    private List<GameObject> pooledObjects = new List<GameObject>();
    private List<GameObject> inactiveObjects = new List<GameObject>();

    //Handles logic whether to spawn or reuse an object when another script calls this method
    public void SpawnObject(Vector3 location, Quaternion rotation)
    {
        if (atMaxCapacity || dynamicMode)
        {
            Reuse(location, rotation);
        }
        else
        {
            Spawn(location, rotation);

        }
    }
    //Increase pool capacity by a given ammount.
    public void IncreaseCapacity(int ammountToIncreaseby)
    {
        maxCapacity += ammountToIncreaseby;
        atMaxCapacity = pooledObjects.Count >= maxCapacity ? true : false;
    }

    private GameObject GetInactiveObject(bool chooseFirst = true)
    {
        //Return a gameobject if the list is full otherwise returns null.
        if (atMaxCapacity || dynamicMode)
        {
            inactiveObjects = MakeListOfInactiveObjects(inactiveObjects);

            //Return null if no objects are inactive.
            if (inactiveObjects.Count == 0)
            {
                return null;
            }
            //Pick the first inactive object from the list.
            if (chooseFirst)
            {
                return inactiveObjects[0];
            }
            //Randomly pick an inactive object from the list.
            else
            {
                return inactiveObjects[Random.Range(0, inactiveObjects.Count)];
            }
        }
        return null;
    }

    private void StoreInObjectPool(GameObject item)
    {
        pooledObjects.Add(item);
        atMaxCapacity = pooledObjects.Count >= maxCapacity ? true : false;
    }

    //Spawns the prefab provided and sets it's location and rotation then stores the spawned game object into the object pool.
    private GameObject Spawn(Vector3 location, Quaternion rotation)
    {
        GameObject spawnedObject = Instantiate(prefabsToSpawnAndPool[Random.Range(0, prefabsToSpawnAndPool.Length)],
                                        location,
                                        rotation);
        StoreInObjectPool(spawnedObject);
        return spawnedObject;

    }
    //Reuses an object from the pool and set it's location and rotation transform before becoming active.
    private void Reuse(Vector3 location, Quaternion rotation)
    {
        GameObject returningObject = GetInactiveObject(chooseFirstObjectInPool);
        if (returningObject)
        {
            Transform returningObjectTransform = returningObject.GetComponent<Transform>();

            returningObjectTransform.position = location;
            returningObjectTransform.rotation = rotation;
            returningObject.SetActive(true);
        }
        else if (dynamicMode)
        {
            Spawn(location, rotation);
        }
    }
    private List<GameObject> MakeListOfInactiveObjects(List<GameObject> listToFill)
    {
        if (listToFill.Count > 0)
        {
            listToFill.Clear();
            //Debug.Log("Given List was cleared before making a new one.");
        }
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i] == null)
            {
                //Debug.LogFormat("Index: {0} is empty.", i);
                pooledObjects.RemoveAt(i);
                //Debug.LogFormat("<color=red>Removed Item Slot:</color> <b>{0}</b>", i);
                i--;
            }
            else if (!pooledObjects[i].activeInHierarchy)
            {
                listToFill.Add(pooledObjects[i]);
            }
        }
        return listToFill;
    }


    //Same functionality as SpawnObject but returns the GameObject for assigning purposes.

    public GameObject SpawnObjectAndAssign(Vector3 location, Quaternion rotation)
    {
        GameObject objectToSpawn;
        if (atMaxCapacity || dynamicMode)
        {
            objectToSpawn = ReuseAndAssign(location, rotation);
            return objectToSpawn;
        }
        else
        {
            objectToSpawn = Spawn(location, rotation);
            return objectToSpawn;
        }
    }

    private GameObject ReuseAndAssign(Vector3 location, Quaternion rotation)
    {
        GameObject returningObject = GetInactiveObject(chooseFirstObjectInPool);
        if (returningObject)
        {
            Transform returningObjectTransform = returningObject.GetComponent<Transform>();

            returningObjectTransform.position = location;
            returningObjectTransform.rotation = rotation;
            returningObject.SetActive(true);
            return returningObject;
        }
        else if (dynamicMode)
        {
            return Spawn(location, rotation);
        }
        else
        {
            return null;
        }
    }
}
