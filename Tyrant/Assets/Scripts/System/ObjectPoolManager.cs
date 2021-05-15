using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference: https://www.raywenderlich.com/847-object-pooling-in-unity

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager instance = null;
    public static ObjectPoolManager Instance { get => instance; }

    [SerializeField]
    private List<ObjectPoolItem> objectPoolItems = null;
    private List<GameObject> pooledObjects = null;

    private void Awake()
    {
        //Singleton pattern
        if (instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        pooledObjects = new List<GameObject>();
    }

    public void InstantiateAll()
    {
        foreach (var item in objectPoolItems)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public void InstantiateObjects(string objectName)
    {
        foreach (var item in objectPoolItems)
        {
            if (item.objectToPool.name.Equals(objectName))
            {
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                }
            }
        }
    }

    public GameObject GetPooledObject(string objectName)
    {
        string cloneName = objectName + "(Clone)";

        if (GameObject.Find(cloneName))
        {
            Debug.LogWarning($"ObjectPoolManager: There is no object called {objectName}...");
            return null;
        }

        foreach (var obj in pooledObjects)
            if (!obj.activeInHierarchy && obj.name.Equals(cloneName))
                return obj;

        foreach (var item in objectPoolItems)
        {
            if (item.objectToPool.name.Equals(objectName) && item.shouldExpand)
            {
                GameObject newObj = (GameObject)Instantiate(item.objectToPool);
                newObj.SetActive(false);
                pooledObjects.Add(newObj);
                return newObj;
            }
        }

        Debug.LogWarning($"ObjectPoolManager: All {objectName} objects are used...");
        return null;
    }
}
