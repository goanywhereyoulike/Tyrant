using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference by https://medium.com/medialesson/simple-service-locator-for-your-unity-project-40e317aad307
public class GameObjectsLocator : MonoBehaviour
{
    public interface IGameObjectRegister
    {
        void RegisterToLocator();
        void UnRegisterToLocator();
    }

    private static GameObjectsLocator instance = null;
    public static GameObjectsLocator Instance { get => instance; }

    private readonly Dictionary<string, List<IGameObjectRegister>> gameObjects = new Dictionary<string, List<IGameObjectRegister>>();

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
    }

    public List<T> Get<T>() where T : IGameObjectRegister
    {
        string key = typeof(T).Name;
        if (!gameObjects.ContainsKey(key))
        {
            Debug.LogError($"{key} not registered with {GetType().Name}");
            throw new System.InvalidOperationException();
        }

        List<T> temp = new List<T>();

        foreach (var item in gameObjects[key])
        {
            temp.Add((T)item);
        }
        return temp;
    }

    public void Register<T>(T Object) where T : IGameObjectRegister
    {
        string key = typeof(T).Name;
        if (gameObjects.ContainsKey(key))
        {
            gameObjects[key].Add(Object);
            return;
        }
        List<IGameObjectRegister> temp = new List<IGameObjectRegister>();

        temp.Add(Object);
        gameObjects.Add(key, temp);
    }

    public void Unregister<T>(T Object) where T : IGameObjectRegister
    {
        string key = typeof(T).Name;
        if (!gameObjects.ContainsKey(key))
        {
            Debug.LogError($"Attempted to unregister gameObject of type {key} which is not registered with the {GetType().Name}.");
            return;
        }

        if (gameObjects[key].Contains((IGameObjectRegister)Object))
        {
            gameObjects[key].Remove((IGameObjectRegister)Object);
        }

        if (gameObjects[key].Count == 0)
        {
            gameObjects.Remove(key);
        }
    }

    //use for test
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            var test = GameObjectsLocator.Instance.Get<SpawnArea>();
            foreach (var item in test)
            {
                Debug.Log(item.name);
            }
        }
    }
}
