using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField]
    private GameObject Indicators;
    [SerializeField]
    private GameObject Targets;
    Renderer render;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (render.isVisible == false)
        {
            if (Indicators.activeSelf == false)
            {
                Indicators.SetActive(true);
            }
            Vector2 dir = Targets.transform.position - transform.position;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, dir);
            if (ray.collider!=null)
            {
                Indicators.transform.position = ray.point;
            }
        }
        else
        {
            if (Indicators.activeSelf==true)
            {
                Indicators.SetActive(false);
            }
        }
    }
}
