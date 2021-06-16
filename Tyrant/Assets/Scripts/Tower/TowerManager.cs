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
    GameObject preTower;
    bool IsPreTowerExist = false;
    // Start is called before the first frame update
    void Start()
    {
        ObjectPoolManager.Instance.InstantiateObjects("TowerBullet");
        player = FindObjectOfType<PlayerMovement>();

    }

    void SetTower(Vector2 target, bool IsAbleToSet)
    {
        
        if (InputManager.Instance.GetKeyDown("PreBuildTower") && !IsPreTowerExist)
        {

            preTower = Instantiate(PreTowerprefab, target, Quaternion.identity);
            preTower.transform.parent = player.transform;
            IsPreTowerExist = true;
        }
        if (InputManager.Instance.GetKeyDown("Esc"))
        {
            BluePrint blue = player.GetComponentInChildren<BluePrint>();
            if (blue)
            {
                Destroy(blue.gameObject);
            }
            IsPreTowerExist = false;

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
                IsPreTowerExist = false;
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

        if (preTower)
        {
            preTower.transform.position = PlayerPos;
        }
      

    }
}
