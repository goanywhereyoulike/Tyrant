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
    private Dictionary<string, List<GameObject>> pooledObjects = null;

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

        pooledObjects = new Dictionary<string, List<GameObject>>();
    }

    public void InstantiateAll()
    {
        foreach (var item in objectPoolItems)
        {
            List<GameObject> gameObjects = new List<GameObject>();
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                gameObjects.Add(obj);
            }
            pooledObjects[item.tagName] = gameObjects;
        }
    }

    public void InstantiateObjects(string tagName)
    {
        bool done = false;
        foreach (var item in objectPoolItems)
        {
            if (item.tagName.Equals(tagName))
            {
                List<GameObject> gameObjects = new List<GameObject>();
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    gameObjects.Add(obj);
                }
                pooledObjects[item.tagName] = gameObjects;
                return;
            }
        }

        if (!done)
        {
            Debug.LogError($"ObjectPoolManager: Cannot instantiate objects because of cannot find {tagName} tagname...");
        }
    }

    public GameObject GetPooledObject(string tagName)
    {
        if (!pooledObjects.ContainsKey(tagName))
        {
            Debug.LogError($"ObjectPoolManager: There is no object called {tagName}...");
            return null;
        }

        foreach (var obj in pooledObjects)
            if (obj.Key.Equals(tagName))
                foreach (var item in obj.Value)
                    if (!item.activeInHierarchy)
                        return item;

        foreach (var item in objectPoolItems)
        {
            if (item.tagName.Equals(tagName) && item.shouldExpand)
            {
                GameObject newObj = (GameObject)Instantiate(item.objectToPool);
                newObj.SetActive(false);
                pooledObjects[tagName].Add(newObj);
                return newObj;
            }
        }

        Debug.LogError($"ObjectPoolManager: All {tagName} objects are used...");
        return null;
    }
}
