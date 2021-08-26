using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PowerUps : MonoBehaviour
{
    protected Player player;
    [SerializeField]
    private float duration;
    public float Duration { get => duration; private set => duration = value; }
    protected virtual void activeEffect() { }
    protected virtual void deactiveEffect() { }

    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<Player>();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            activeEffect();
            Debug.Log("effect actived");
            yield return new WaitForSeconds(duration);
            deactiveEffect();
            Debug.Log("effect deactived");
            gameObject.SetActive(false);
        }
    }
}
