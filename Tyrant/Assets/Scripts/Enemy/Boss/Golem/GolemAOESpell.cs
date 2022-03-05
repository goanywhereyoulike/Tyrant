using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAOESpell : MonoBehaviour
{
    [SerializeField]
    private float offsetB2NAnimations = 10.0f;
    private void FixedOffset()
    {
        gameObject.transform.position = gameObject.transform.position + Vector3.up * offsetB2NAnimations;
    }

}
