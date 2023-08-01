using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://gameprogrammingpatterns.com/object-pool.html

/// <summary>
/// Currently not checking if this object is enable or not since I plan to use it only for the regular bullet shells
/// Those will be enabled by default.
/// 
/// In case I want to ever do that for something that is otherwise disbled, I would need to perform a check
/// in the class that uses the SpawnObject(....) function
/// </summary>

public class ObjectPooling : MonoBehaviour
{
    [SerializeField]
    private int _poolSize;
    private int _currentSize;

    [SerializeField]
    protected GameObject objectsToSpawn;
    protected Queue<GameObject> objectPoolQueue;
    private void Awake()
    {
        objectPoolQueue = new Queue<GameObject>();
    }

    public virtual GameObject SpawnObject(GameObject currentObject = null)
    {
        if (currentObject == null)
            currentObject = objectsToSpawn;

        GameObject spawnedObject = null;

        if(_currentSize < _poolSize) // we can create more objects so we can simply add a new object and instantiate it
        {
            // here transform.position is the position of gun
            spawnedObject = Instantiate(currentObject, transform.position, Quaternion.identity);
            spawnedObject.name = currentObject.name + "_" + _currentSize;
            _currentSize++;
        }
        else // we have a full queue and we want to reuse the first object. i.e. the oldest object in the queue
        {
            spawnedObject = objectPoolQueue.Dequeue();
            spawnedObject.transform.position = transform.position; //position of our object pool
            spawnedObject.transform.rotation = Quaternion.identity;
        }

        objectPoolQueue.Enqueue(spawnedObject); // calling the object pool and enqueue it
        return spawnedObject;
    }
}
