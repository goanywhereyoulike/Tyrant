using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrap_ArrowPortal : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ArrowPrefab;
    float WaitFire = 0.0f;
    public float FireRate;

    [SerializeField]
    SpriteRenderer sprite;

    void Start()
    {
        ObjectPoolManager.Instance.InstantiateObjects("TrapArrow");
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        WaitFire += Time.deltaTime;
        if (WaitFire > FireRate)
        {
            GameObject arrow = ObjectPoolManager.Instance.GetPooledObject("TrapArrow");
            if (arrow)
            {
                arrow.transform.position = transform.position;
                Setdirection(arrow);
                arrow.SetActive(true);


                //StartCoroutine(DestroyBullet(arrow));
                WaitFire = 0.0f;
            }

        }


    }

    void Setdirection(GameObject arrow)
    {
        Quaternion rotation = Quaternion.identity;
        if (sprite.sprite.name == "arrowportal_top")
        {
            rotation.eulerAngles = Vector3.zero;
            arrow.transform.rotation = rotation;
            arrow.GetComponent<LevelTrap_Arrow>().SetDirection(1);

        }
        else if (sprite.sprite.name == "arrowportal_down")
        {
            rotation.eulerAngles = new Vector3(00.0f,0.0f,180.0f);
            arrow.transform.rotation = rotation;
            arrow.GetComponent<LevelTrap_Arrow>().SetDirection(2);

        }
        else if (sprite.sprite.name == "arrowportal_left")
        {
            rotation.eulerAngles = new Vector3(00.0f, 0.0f, 90.0f);
            arrow.transform.rotation = rotation;
            arrow.GetComponent<LevelTrap_Arrow>().SetDirection(3);

        }
        else if (sprite.sprite.name == "arrowportal_right")
        {
            rotation.eulerAngles = new Vector3(00.0f, 0.0f, -90.0f);
            arrow.transform.rotation = rotation;
            arrow.GetComponent<LevelTrap_Arrow>().SetDirection(4);
        }


    }

    IEnumerator DestroyBullet(GameObject arrow)
    {
        yield return new WaitForSeconds(1.0f);
        arrow.SetActive(false);
    }




}
