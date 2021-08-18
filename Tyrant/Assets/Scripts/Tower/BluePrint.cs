using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrint : MonoBehaviour
{

    public bool IsAbleToSet;

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
