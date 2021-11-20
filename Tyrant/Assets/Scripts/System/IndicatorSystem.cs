using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject target = null;

    public static void DisableIndicator(GameObject obj)
    {
        Indicator indicator = obj.GetComponentInChildren<Indicator>();
        if (indicator)
        {
            indicator.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (GameObjectsLocator.Instance.Get<Indicator>() == null)
            return;

        foreach (var indicator in GameObjectsLocator.Instance.Get<Indicator>())
        {
            if (!indicator.ParentRendererer.isVisible)
            {
                if (!indicator.SpriteRenderer.enabled)
                    indicator.SpriteRenderer.enabled = true;

                Vector2 direction = target.transform.position - indicator.transform.parent.position;

                RaycastHit2D ray = Physics2D.Raycast(indicator.transform.parent.position, direction, Mathf.Infinity, LayerMask.GetMask("CamBox"));

                if (ray.collider != null)
                {
                    indicator.gameObject.transform.position = ray.point;
                }
            }
            else
            {
                if (indicator.SpriteRenderer.enabled)
                    indicator.SpriteRenderer.enabled = false;
            }
        }
    }
}
