using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour, IDamageable , GameObjectsLocator.IGameObjectRegister
{
    public float health = 50.0f;
    public float damge = 10.0f;

    public void RegisterToLocator()
    {
        GameObjectsLocator.Instance.Register<target>(this);
    }

    private void Update()
    {
        RegisterToLocator();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0.0f)
        {
            UnRegisterToLocator();
            gameObject.SetActive(false);
        }
    }

    public void UnRegisterToLocator()
    {
        GameObjectsLocator.Instance.Unregister<target>(this);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            IDamageable Targets = collider.gameObject.GetComponent<IDamageable>();
            Targets.TakeDamage(damge);
        }

    }

}
