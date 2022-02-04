using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
