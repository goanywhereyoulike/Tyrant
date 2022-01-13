using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrapUnlock : MonoBehaviour
{
    public Vector2 AnimatePos;
    public bool isInfinite = false;
    public int index = -1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
            // ManagerSwitch mangerSwitch = FindObjectOfType<ManagerSwitch>();

            //TrapManager TrapMngr = FindObjectOfType<TrapManager>();

            TrapManager.Instance.AddTrap(index);
            gameObject.transform.DOShakeScale(0.8f).OnComplete(()=> { transform.DORewind(); });

            if (!isInfinite)
            {
                gameObject.SetActive(false);
            }
            // Destroy(gameObject);

        }
    }
}
