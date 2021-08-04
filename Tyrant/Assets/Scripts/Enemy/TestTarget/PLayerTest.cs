using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerTest : MonoBehaviour,IDamageable,GameObjectsLocator.IGameObjectRegister
{
    public string Pname;

    public float time;
    public void RegisterToLocator()
    {
        GameObjectsLocator.Instance.Register<PLayerTest>(this);      
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public void UnRegisterToLocator()
    {
        GameObjectsLocator.Instance.Unregister<PLayerTest>(this);
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        RegisterToLocator();
        StartCoroutine(deactive());
       
    }

    IEnumerator deactive()
    {
        yield return new WaitForSeconds(time);
        var enemy = GameObjectsLocator.Instance.Get<Enemy>();
        UnRegisterToLocator();
    }
    // Update is called once per frame
    //void Update()
    //{
    //    if(Input.GetKey(KeyCode.Space ))
    //    {
    //        var test = GameObjectsLocator.Instance.Get<PLayerTest>();
    //        foreach (var item in test)
    //        {
    //            Debug.Log(item.Pname);
    //        }
    //    }
    //}
}
