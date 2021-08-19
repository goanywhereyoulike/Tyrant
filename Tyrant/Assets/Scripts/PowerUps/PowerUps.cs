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
    //protected virtual void onTrigger(Collider2D collision) { }
    protected virtual void activeEffect() { }
    protected virtual void deactiveEffect() { }

    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<Player>();
            //onTrigger(collision);
            activeEffect();
            Debug.Log("effect actived");
            //StartCoroutine(waitForDuration());
            yield return new WaitForSeconds(duration);
            deactiveEffect();
            Debug.Log("effect deactived");
            gameObject.SetActive(false);
        }
    }

    IEnumerator waitForDuration()
    {
        yield return new WaitForSeconds(duration);
    }
}
