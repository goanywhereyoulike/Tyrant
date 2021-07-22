using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    PlayerMovement player;
    SpriteRenderer TowerSprite;
    public List<TowerTemplate> Towers = new List<TowerTemplate>();

    [SerializeField]
    private Text Coinnumber;

    [SerializeField]
    Image PreTowerSprite;

    [SerializeField]
    Image PreCannonTowerSprite;

    [SerializeField]
    Image PreChainTowerSprite;

    public GameObject Towerprefab;
    public GameObject CannonTowerprefab;
    public GameObject ChainTowerprefab;

    public GameObject PreTowerprefab;
    public GameObject PreCannonTowerprefab;
    public GameObject PreChainTowerprefab;

    private GameObject PreTower;

    public Vector3 offset;


    private int TowerNumber = 0;
    List<bool> IsAbleToSet = new List<bool>(3);
    GameObject preTower;
    bool IsPreTowerExist = false;
    // Start is called before the first frame update
    void Start()
    {
        ObjectPoolManager.Instance.InstantiateObjects("TowerBullet");
        ObjectPoolManager.Instance.InstantiateObjects("CannonTowerBullet");
        ObjectPoolManager.Instance.InstantiateObjects("ChainTowerBullet");
        player = FindObjectOfType<PlayerMovement>();
        for (int i = 0; i < 3; i++)
        {
            IsAbleToSet.Add(false);
        }

    }

    void DestroyTower()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (InputManager.Instance.GetKeyDown("DestroyTower"))
            {
                if (hit.collider.gameObject.tag == "Tower")
                {
                    Destroy(hit.collider.gameObject.transform.parent.gameObject);

                }
            }


        }
       
    }

    void CheckCoin()
    {

        if (player.GetComponent<Player>().coin < Towers[0].price)
        {
            PreTowerSprite.color = Color.red;
            IsAbleToSet[0] = false;
        }
        if (player.GetComponent<Player>().coin < Towers[1].price)
        {
            PreCannonTowerSprite.color = Color.red;
            IsAbleToSet[1] = false;
        }
        if (player.GetComponent<Player>().coin < Towers[2].price)
        {
            PreChainTowerSprite.color = Color.red;
            IsAbleToSet[2] = false;
        }
        if (player.GetComponent<Player>().coin >= Towers[0].price)
        {
            PreTowerSprite.color = Color.green;
            IsAbleToSet[0] = true;
        }
        if (player.GetComponent<Player>().coin >= Towers[1].price)
        {
            PreCannonTowerSprite.color = Color.green;
            IsAbleToSet[1] = true;
        }
        if (player.GetComponent<Player>().coin >= Towers[2].price)
        {
            PreChainTowerSprite.color = Color.green;
            IsAbleToSet[2] = true;
        }



    }


    void SetTower(Vector2 target)
    {

        if (InputManager.Instance.GetKeyDown("PreBuildTower") && !IsPreTowerExist && IsAbleToSet[0])
        {

            PreTower = Instantiate(PreTowerprefab, target, Quaternion.identity);
            PreTower.transform.parent = player.transform;
            IsPreTowerExist = true;
            TowerNumber = 1;

        }

        if (InputManager.Instance.GetKeyDown("PreBuildCannonTower") && !IsPreTowerExist && IsAbleToSet[1])
        {

            PreTower = Instantiate(PreCannonTowerprefab, target, Quaternion.identity);
            PreTower.transform.parent = player.transform;
            IsPreTowerExist = true;
            TowerNumber = 2;

        }

        if (InputManager.Instance.GetKeyDown("PreBuildChainTower") && !IsPreTowerExist && IsAbleToSet[2])
        {

            PreTower = Instantiate(PreChainTowerprefab, target, Quaternion.identity);
            PreTower.transform.parent = player.transform;
            IsPreTowerExist = true;
            TowerNumber = 3;

        }

        if (InputManager.Instance.GetKeyDown("dropTower") && IsPreTowerExist)
        {
            BluePrint blue = player.GetComponentInChildren<BluePrint>();
            if (blue)
            {
                Destroy(blue.gameObject);
            }
            IsPreTowerExist = false;

        }

        if (PreTower)
        {
            BluePrint bluePrint = PreTower.GetComponent<BluePrint>();
            if (bluePrint)
            {
                TowerSprite = bluePrint.gameObject.GetComponent<SpriteRenderer>();
            }


            if (bluePrint && bluePrint.IsAbleToSet)
            {
                TowerSprite.color = Color.green;
                if (InputManager.Instance.GetKeyDown("BuildTower"))
                {
                    if (TowerNumber == 1)
                    {
                        Instantiate(Towerprefab, target, Quaternion.identity);
                        player.GetComponent<Player>().coin -= 50;
                    }
                    if (TowerNumber == 2)
                    {
                        Instantiate(CannonTowerprefab, target, Quaternion.identity);
                        player.GetComponent<Player>().coin -= 100;
                    }
                    if (TowerNumber == 3)
                    {
                        Instantiate(ChainTowerprefab, target, Quaternion.identity);
                        player.GetComponent<Player>().coin -= 150;
                    }

                    Destroy(bluePrint.gameObject);
                    IsPreTowerExist = false;
                }
            }
            else if (bluePrint && !bluePrint.IsAbleToSet)
            {
                TowerSprite.color = Color.red;

            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        Coinnumber.text = player.GetComponent<Player>().coin.ToString();

        CheckCoin();
        Vector2 PlayerPos = player.transform.position + offset;
        SetTower(PlayerPos);

        if (PreTower)
        {
            PreTower.transform.position = PlayerPos;
        }
        DestroyTower();
    }
}
