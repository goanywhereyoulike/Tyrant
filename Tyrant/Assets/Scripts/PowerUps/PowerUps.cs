using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider2D))]
public class PowerUps : MonoBehaviour
{
    protected bool isTriggered = false;
    protected Player player;
    [SerializeField]
    private float duration;
    public float Duration { get => duration; private set => duration = value; }
    protected virtual void activeEffect() { }
    protected virtual void deactiveEffect()
    {
   
    }
    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            player = collision.gameObject.GetComponent<Player>();
            //gameObject.GetComponent<SpriteRenderer>().enabled = false;
            activeEffect();
            gameObject.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => { gameObject.GetComponent<SpriteRenderer>().enabled = false; });
            //Debug.Log("effect actived");
            if (isTriggered)
            {
                yield return new WaitForSeconds(duration);
                deactiveEffect();
                //Debug.Log("effect deactived");
                gameObject.SetActive(false);
            }
        }
    }
}
