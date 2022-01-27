using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIParticleEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem ps;
    void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        ps.Stop();
    }

    // Update is called once per frame
}
