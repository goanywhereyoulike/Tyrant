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
    private int TowerNumberLimit = 5;

    [SerializeField]
    private Image TowerPanel;

    //[SerializeField]
    //private Image TrapPanel;

    //[SerializeField]
    //private Text Coinnumber;
    [SerializeField]
    private Text TowerNumberText;

    [SerializeField]
    private Text Tower1Price;

    [SerializeField]
    private Text Tower2Price;

    [SerializeField]
    private Text Tower3Price;

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


    private int TowerIndex = 0;
    private int TowerNumber = 0;
    private bool IsReachTowerNumberLimit = false;


    List<bool> IsAbleToSet = new List<bool>(3);
    GameObject preTower;
    bool IsPreTowerExist = false;
    // Start is called before the first frame update
    void Start()
    {
        TowerPanel.gameObject.SetActive(true);

        ObjectPoolManager.Instance.InstantiateObjects("TowerBullet");
        ObjectPoolManager.Instance.InstantiateObjects("CannonTowerBullet");
        ObjectPoolManager.Instance.InstantiateObjects("ChainTowerBullet");
        player = FindObjectOfType<PlayerMovement>();
        for (int i = 0; i < 3; i++)
        {
            IsAbleToSet.Add(false);
        }

        Tower1Price.text = Towers[0].price.ToString();
        Tower2Price.text = Towers[1].price.ToString();
        Tower3Price.text = Towers[2].price.ToString();
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
    public void ResetTowerNumber()
    {
        TowerNumber = 0;
    
    }

    void CheckNumberLimit()
    {
        if (TowerNumber >= TowerNumberLimit)
        {
            for (int i = 0; i < IsAbleToSet.Count; ++i)
            {
                IsAbleToSet[i] = false;
                
            }
            PreTowerSprite.color = Color.red;
            PreCannonTowerSprite.color = Color.red;
            PreChainTowerSprite.color = Color.red;
            IsReachTowerNumberLimit = true;
        }
    
    
    }
    void CheckCoin()
    {
        if (IsReachTowerNumberLimit)
        {
            return;
        }
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
            TowerIndex = 1;

        }

        if (InputManager.Instance.GetKeyDown("PreBuildCannonTower") && !IsPreTowerExist && IsAbleToSet[1])
        {

            PreTower = Instantiate(PreCannonTowerprefab, target, Quaternion.identity);
            PreTower.transform.parent = player.transform;
            IsPreTowerExist = true;
            TowerIndex = 2;

        }

        if (InputManager.Instance.GetKeyDown("PreBuildChainTower") && !IsPreTowerExist && IsAbleToSet[2])
        {

            PreTower = Instantiate(PreChainTowerprefab, target, Quaternion.identity);
            PreTower.transform.parent = player.transform;
            IsPreTowerExist = true;
            TowerIndex = 3;

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
                    if (TowerIndex == 1)
                    {
                        Instantiate(Towerprefab, target, Quaternion.identity);
                        player.GetComponent<Player>().coin -= 50;
                    }
                    if (TowerIndex == 2)
                    {
                        Instantiate(CannonTowerprefab, target, Quaternion.identity);
                        player.GetComponent<Player>().coin -= 100;
                    }
                    if (TowerIndex == 3)
                    {
                        Instantiate(ChainTowerprefab, target, Quaternion.identity);
                        player.GetComponent<Player>().coin -= 150;
                    }

                    Destroy(bluePrint.gameObject);
                    IsPreTowerExist = false;
                    TowerNumber++;
                }
            }
            else if (bluePrint && !bluePrint.IsAbleToSet)
            {
                TowerSprite.color = Color.red;

            }
        }


    }

    //void ChangePanel()
    //{

    //    if (InputManager.Instance.GetKeyDown("ChangeTowerPanel"))
    //    {
    //        if (TowerPanel.gameObject.activeSelf == true)
    //        {
    //            TowerPanel.gameObject.SetActive(false);
    //            TrapPanel.gameObject.SetActive(true);
    //        }
    //        else if (TrapPanel.gameObject.activeSelf == true)
    //        {
    //            TowerPanel.gameObject.SetActive(true);
    //            TrapPanel.gameObject.SetActive(false);
    //        }
    //    }



    //}

    // Update is called once per frame
    void Update()
    {

        //ChangePanel();
        //Coinnumber.text = player.GetComponent<Player>().coin.ToString();
        TowerNumberText.text = TowerNumber.ToString();
        CheckNumberLimit();
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
