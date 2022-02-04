using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Blood : MonoBehaviour
{
    private SpriteRenderer sp;
    public Sprite[] bloodStain;
    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        int rand = Random.Range(0, bloodStain.Length);
        sp.sprite = bloodStain[rand];
        sp.DOFade(0.0f,5.0f).OnComplete(()=> { Destroy(gameObject); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
