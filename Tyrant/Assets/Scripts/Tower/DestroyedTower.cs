using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedTower : MonoBehaviour,GameObjectsLocator.IGameObjectRegister
{
    public void RegisterToLocator()
    {
        GameObjectsLocator.Instance.Register<DestroyedTower>(this);
    }

    public void UnRegisterToLocator()
    {
        GameObjectsLocator.Instance.Unregister<DestroyedTower>(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        RegisterToLocator();
    }

}
