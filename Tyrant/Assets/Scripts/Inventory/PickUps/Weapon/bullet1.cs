using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet1 : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 50;
    public float range = 5.0f;
    Vector3 direction = Vector3.zero;
    public Vector3 startPosition { get; set; }
 
    public Vector3 Direction { get { return direction; } set { direction = value; } }

    //private void Start()
    //{
    //    range = 1.0f;
    //}
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        if ((transform.position - startPosition).magnitude >= range)
        {
            Debug.Log("destoryed bullets");
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.tag == "Enemy")
       {
           Debug.Log(damage);
           IDamageable Enemy = collision.gameObject.GetComponent<IDamageable>();
           Enemy.TakeDamage(damage);
           gameObject.SetActive(false);
       }
    }
}


    
    



