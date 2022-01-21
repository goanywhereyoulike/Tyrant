using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrapUnlock : MonoBehaviour
{
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


            if (!isInfinite)
            {
                gameObject.transform.DOScale(Vector3.zero,0.5f).OnComplete(() => { gameObject.SetActive(false); });
              
            }
            else
            {
                gameObject.transform.DOShakeScale(0.8f).OnComplete(() => { transform.DORewind(); });

            }
            // Destroy(gameObject);

        }
    }
}
