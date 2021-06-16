using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRange : MonoBehaviour
{
    public float range = 0.0f;
    // Start is called before the first frame update
    private void OnDrawGizmosSelected()
    {
      Gizmos.DrawWireSphere(transform.position, range);
    }
}
