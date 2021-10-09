using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingTowerBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletDamage = 5.0f;
    [SerializeField]
    private Texture[] textures;
    private int animationStep;

    [SerializeField]
    float fps = 30.0f;

    private float fpsCounter;
    private LineRenderer lineRenderer;

    Transform target;

    private void Awake()
    {

        lineRenderer = GetComponent<LineRenderer>();

    }
    public void SetTarget(Vector3 startpos, Transform newTarget)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startpos);
        target = newTarget;

    
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(1, target.position);
        fpsCounter += Time.deltaTime;
        if (fpsCounter >= 1.0f / fps)
        {
            animationStep = (animationStep + 1) % textures.Length;
            lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);

            fpsCounter = 0.0f;
        
        }
    }
}
