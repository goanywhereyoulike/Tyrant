using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    PlayerMovement player;
    SpriteRenderer TowerSprite;
    public GameObject Towerprefab;
    public GameObject PreTowerprefab;
    public Vector3 offset;
    bool IsAbleToSet = true;

    // Start is called before the first frame update
    void Start()
    {
        ObjectPoolManager.Instance.InstantiateObjects("TowerBullet");
        player = FindObjectOfType<PlayerMovement>();

    }

    void SetTower(Vector2 target, bool IsAbleToSet)
    {
        //bool IsPreTowerExist = false;
        if (InputManager.Instance.GetKeyDown("PreBuildTower"))
        {

            GameObject preTower = Instantiate(PreTowerprefab, target, Quaternion.identity);
            preTower.transform.parent = player.transform;
        }
        if (InputManager.Instance.GetKeyDown("Esc"))
        {
            BluePrint blue = player.GetComponentInChildren<BluePrint>();
            if (blue)
            {
                Destroy(blue.gameObject);
            }


        }

        BluePrint bluePrint = player.GetComponentInChildren<BluePrint>();
        if (bluePrint)
        {
            TowerSprite = bluePrint.gameObject.GetComponent<SpriteRenderer>();
        }
        

        if (bluePrint && bluePrint.IsAbleToSet)
        {
            TowerSprite.color = Color.green;
            if (InputManager.Instance.GetKeyDown("BuildTower"))
            {
                Instantiate(Towerprefab, target, Quaternion.identity);
                Destroy(bluePrint.gameObject);

            }
        }
        else if (bluePrint && !bluePrint.IsAbleToSet)
        {
            TowerSprite.color = Color.red;

        }

    }




    // Update is called once per frame
    void Update()
    {
        Vector2 PlayerPos = player.transform.position + offset;
        SetTower(PlayerPos, IsAbleToSet);



    }
}
