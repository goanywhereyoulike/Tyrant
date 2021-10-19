using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XBullet : MonoBehaviour
{
    public Vector3 StartPosition { get; set; }
    public Vector3 Direction { get; set; }
    public float BulletSpeed;
    public float Range { get; set; }
    public int Damage { get; set; }

    // Update is called once per frame
    private void Update()
    {
        transform.position += Direction * BulletSpeed * Time.deltaTime;
        if ((transform.position - StartPosition).magnitude >= Range)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Tower" || collider.gameObject.tag == "Base")
        {
            IDamageable Targets = collider.gameObject.GetComponent<IDamageable>();
            if (Targets == null)
            {
                Targets = collider.gameObject.GetComponentInChildren<IDamageable>();
            }
            Targets.TakeDamage(Damage);
            gameObject.SetActive(false);
            Debug.Log("attack");
        }

        if (collider.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
        }
    }
}
