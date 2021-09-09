using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluePrint : MonoBehaviour
{
    [SerializeField]
    Image TowerRangeUI;

    public bool IsAbleToSet;
    public TowerTemplate tower;

    private void Start()
    {
        TowerRangeUI.rectTransform.sizeDelta = new Vector2(tower.distanceToShoot, tower.distanceToShoot);

        //TowerRangeUI.rectTransform.SetSizeWithCurrentAnchors(TowerRangeUI.rectTransform.Axis);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "TowerBlockArea" || collision.gameObject.tag == "Tower" || collision.gameObject.tag == "Trap")
        {
            IsAbleToSet = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TowerBlockArea" || collision.gameObject.tag == "Tower" || collision.gameObject.tag == "Trap")
        {
            IsAbleToSet = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "TowerBlockArea" || collision.gameObject.tag == "Tower" || collision.gameObject.tag == "Trap")
        {
            IsAbleToSet = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "TowerBlockArea" || collision.gameObject.tag == "Tower" || collision.gameObject.tag == "Trap")
        {
            IsAbleToSet = true;
        }
    }

}
