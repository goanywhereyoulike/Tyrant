using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{

    enum TowerType {Basic,Cannon,Chain,Lighting,Taunt };

    TowerType towerType;

    List<Tower> towers = new List<Tower>();

    public TowerLimitTemplate towerroomInfos;
    PlayerMovement player;
    SpriteRenderer TowerSprite;
    public List<TowerTemplate> TowerTemplates = new List<TowerTemplate>();

    [SerializeField]
    private Animator TowerNumberWarning;


    private int TowerNumberLimit;

    [SerializeField]
    private Text TowerNumberText;

    [SerializeField]
    private Image TowerPanel;

    [SerializeField]
    private Image Tower1Cover;

    [SerializeField]
    private Image Tower2Cover;

    [SerializeField]
    private Image Tower3Cover;

    [SerializeField]
    private Image Tower4Cover;

    [SerializeField]
    private Image Tower5Cover;

    [SerializeField]
    private Text Tower1Price;

    [SerializeField]
    private Text Tower2Price;

    [SerializeField]
    private Text Tower3Price;

    [SerializeField]
    private Text Tower4Price;

    [SerializeField]
    private Text Tower5Price;

    [SerializeField]
    Image PreTowerSprite;

    [SerializeField]
    Image PreCannonTowerSprite;

    [SerializeField]
    Image PreChainTowerSprite;

    [SerializeField]
    Image PreLightingTowerSprite;

    [SerializeField]
    Image PreTauntTowerSprite;

    //Select choice for designer
    [SerializeField]
    private GameObject Select1Cover;

    [SerializeField]
    private GameObject Select2Cover;

    [SerializeField]
    private GameObject Select3Cover;

    [SerializeField]
    private GameObject Select4Cover;

    [SerializeField]
    private GameObject Select5Cover;




    public GameObject Towerprefab;
    public GameObject CannonTowerprefab;
    public GameObject ChainTowerprefab;
    public GameObject LightingTowerprefab;
    public GameObject TauntTowerprefab;

    public GameObject PreTowerprefab;
    public GameObject PreCannonTowerprefab;
    public GameObject PreChainTowerprefab;
    public GameObject PreLightingTowerprefab;
    public GameObject PreTauntTowerprefab;

    private GameObject PreTower;

    public Vector3 offset;


    private int TowerIndex = 0;
    private int TowerNumber = 0;
    [SerializeField]
    private bool IsReachTowerNumberLimit = false;

    int roomNumber = 0;
    private bool roomClear;
    private bool isRoomCheck;
    private int checkcount;
    private int lastroom;
    [SerializeField]
    List<bool> SelectAbleToSet = new List<bool>(5);

    [SerializeField]
    private List<bool> IsAbleToSet = new List<bool>(5);
    GameObject preTower;
    public bool IsPreTowerExist = false;
    // Start is called before the first frame update
    void Start()
    {
        TowerPanel.gameObject.SetActive(true);
        RoomManager.Instance.RoomChanged += RoomChanged;
        ObjectPoolManager.Instance.InstantiateObjects("TowerBullet");
        ObjectPoolManager.Instance.InstantiateObjects("CannonTowerBullet");
        ObjectPoolManager.Instance.InstantiateObjects("ChainTowerBullet");
        ObjectPoolManager.Instance.InstantiateObjects("LightingTowerBullet");
        player = FindObjectOfType<PlayerMovement>();
        for (int i = 0; i < 5; i++)
        {
            IsAbleToSet.Add(false);
            SelectAbleToSet.Add(false);
        }

        Tower1Price.text = TowerTemplates[0].price.ToString();
        Tower2Price.text = TowerTemplates[1].price.ToString();
        Tower3Price.text = TowerTemplates[2].price.ToString();
        Tower4Price.text = TowerTemplates[3].price.ToString();
        Tower5Price.text = TowerTemplates[4].price.ToString();



        TowerNumberLimit = towerroomInfos.towerroomInfo[0].TowerNumber;
        //TowerNumlimit.text = TowerNumberLimit.ToString();
        TowerNumberText.text = SetTowerNumberUI(TowerNumber, TowerNumberLimit).ToString();


        //towerroomInfos.towerroomInfo.Reverse(RoomManager.Instance.);
    }

    public void DecreaseTowerlimit()
    {
        TowerNumber--;
        if (TowerNumber < 0)
        {
            TowerNumber = 0;
        }

        TowerNumberText.text = SetTowerNumberUI(TowerNumber, TowerNumberLimit).ToString();
        IsReachTowerNumberLimit = false;
    }


    string SetTowerNumberUI(int towernumber, int towerlimit)
    {
        if (towerlimit > 10000)
        {
            string towernumbertext = TowerNumber.ToString() + "/" + "\u221E";

            return towernumbertext;
        }
        string towerinformation = towernumber + "/" + towerlimit;
        return towerinformation;

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

    void ClearAll()
    {
        ResetTowerNumber();
        var dtowers = GameObjectsLocator.Instance.Get<DestroyedTower>();
        var towers = GameObjectsLocator.Instance.Get<Tower>();
        if (towers != null)
        {
            foreach (var tower in towers)
            {
                if (tower)
                {
                    Destroy(tower.transform.parent.gameObject);
                    tower.UnRegisterToLocator();

                }


            }
        }

        if (dtowers != null)
        {
            foreach (var dtower in dtowers)
            {
                if (dtower)
                {
                    Destroy(dtower.gameObject);
                    dtower.UnRegisterToLocator();
                }


            }
        }



    }

    private void RoomChanged(int changeId)
    {
        ClearAll();
        roomNumber = changeId;
        TowerNumberLimit = towerroomInfos.towerroomInfo[roomNumber].TowerNumber;
        TowerNumberText.text = SetTowerNumberUI(TowerNumber, TowerNumberLimit).ToString();
        //CheckSelection();
    }

    public void ResetTowerNumber()
    {
        TowerNumber = 0;
        IsReachTowerNumberLimit = false;
    }

    void SetTowerLimit(int num)
    {
        TowerNumberLimit = num;
    }
    void ShowWarningUIForTowerNumber()
    {
        if (TowerNumber >= TowerNumberLimit)
        {
            if (TowerNumberWarning.gameObject.activeSelf)
            {
                TowerNumberWarning.SetTrigger("Show");
            }

            //TowerWarning.Play("TowerWarningUIDefault");
        }
    }
    void CheckNumberLimit()
    {
        if (TowerNumber >= TowerNumberLimit)
        {
            for (int i = 0; i < IsAbleToSet.Count; ++i)
            {
                IsAbleToSet[i] = false;
                SelectAbleToSet[i] = true;

            }
            PreTowerSprite.color = Color.red;
            PreCannonTowerSprite.color = Color.red;
            PreChainTowerSprite.color = Color.red;
            PreLightingTowerSprite.color = Color.red;
            PreTauntTowerSprite.color = Color.red;
            IsReachTowerNumberLimit = true;
        }

    }

    void CheckSelection()
    {
        SelectAbleToSet[0] = towerroomInfos.towerroomInfo[roomNumber].BasicTower;
        SelectAbleToSet[1] = towerroomInfos.towerroomInfo[roomNumber].CannonTower;
        SelectAbleToSet[2] = towerroomInfos.towerroomInfo[roomNumber].ChainTower;
        SelectAbleToSet[3] = towerroomInfos.towerroomInfo[roomNumber].LightingTower;
        SelectAbleToSet[4] = towerroomInfos.towerroomInfo[roomNumber].TauntTower;

        //Select Normal Tower
        if (SelectAbleToSet[0])
        {
            Select1Cover.SetActive(false);
            IsAbleToSet[0] = true;
        }
        else
        {
            Select1Cover.SetActive(true);
            IsAbleToSet[0] = false;


        }
        //Select Cannon Tower
        if (SelectAbleToSet[1])
        {
            Select2Cover.SetActive(false);
            IsAbleToSet[1] = true;
        }
        else
        {
            Select2Cover.SetActive(true);
            IsAbleToSet[1] = false;


        }
        //Select Chain Tower
        if (SelectAbleToSet[2])
        {
            Select3Cover.SetActive(false);
            IsAbleToSet[2] = true;
        }
        else
        {
            Select3Cover.SetActive(true);
            IsAbleToSet[2] = false;
        }

        //Select Lighting Tower
        if (SelectAbleToSet[3])
        {
            Select4Cover.SetActive(false);
            IsAbleToSet[3] = true;
        }
        else
        {
            Select4Cover.SetActive(true);
            IsAbleToSet[3] = false;

        }

        //Select Taunt Tower
        if (SelectAbleToSet[4])
        {
            Select5Cover.SetActive(false);
            IsAbleToSet[4] = true;
        }
        else
        {
            Select5Cover.SetActive(true);
            IsAbleToSet[4] = false;

        }

        //IsReachTowerNumberLimit = true;
    }
    void CheckCoin()
    {
        if (IsReachTowerNumberLimit)
        {
            return;
        }

        if (player.GetComponent<Player>().coin < TowerTemplates[0].price)
        {
            PreTowerSprite.color = Color.red;
            IsAbleToSet[0] = false;
        }
        if (player.GetComponent<Player>().coin < TowerTemplates[1].price)
        {
            PreCannonTowerSprite.color = Color.red;
            IsAbleToSet[1] = false;
        }
        if (player.GetComponent<Player>().coin < TowerTemplates[2].price)
        {
            PreChainTowerSprite.color = Color.red;
            IsAbleToSet[2] = false;
        }
        if (player.GetComponent<Player>().coin < TowerTemplates[3].price)
        {
            PreLightingTowerSprite.color = Color.red;
            IsAbleToSet[3] = false;
        }
        if (player.GetComponent<Player>().coin < TowerTemplates[4].price)
        {
            PreTauntTowerSprite.color = Color.red;
            IsAbleToSet[4] = false;
        }
        if (player.GetComponent<Player>().coin >= TowerTemplates[0].price && SelectAbleToSet[0])
        {
            PreTowerSprite.color = Color.white;
            IsAbleToSet[0] = true;
        }
        if (player.GetComponent<Player>().coin >= TowerTemplates[1].price && SelectAbleToSet[1])
        {
            PreCannonTowerSprite.color = Color.white;
            IsAbleToSet[1] = true;
        }
        if (player.GetComponent<Player>().coin >= TowerTemplates[2].price && SelectAbleToSet[2])
        {
            PreChainTowerSprite.color = Color.white;
            IsAbleToSet[2] = true;
        }
        if (player.GetComponent<Player>().coin >= TowerTemplates[3].price && SelectAbleToSet[3])
        {
            PreLightingTowerSprite.color = Color.white;
            IsAbleToSet[3] = true;
        }
        if (player.GetComponent<Player>().coin >= TowerTemplates[4].price && SelectAbleToSet[4])
        {
            PreTauntTowerSprite.color = Color.white;
            IsAbleToSet[4] = true;
        }

    }


    void SetTower(Vector2 target)
    {

        if (InputManager.Instance.GetKeyDown("PreBuildTower"))
        {
            ShowWarningUIForTowerNumber();
            if (!IsPreTowerExist && IsAbleToSet[0])
            {
                PreTower = Instantiate(PreTowerprefab, target, Quaternion.identity);
                PreTower.transform.parent = player.transform;
                IsPreTowerExist = true;
                TowerIndex = 1;
            }
        }

        if (InputManager.Instance.GetKeyDown("PreBuildCannonTower"))
        {
            ShowWarningUIForTowerNumber();
            if (!IsPreTowerExist && IsAbleToSet[1])
            {
                PreTower = Instantiate(PreCannonTowerprefab, target, Quaternion.identity);
                PreTower.transform.parent = player.transform;
                IsPreTowerExist = true;
                TowerIndex = 2;
            }

        }

        if (InputManager.Instance.GetKeyDown("PreBuildChainTower"))
        {
            ShowWarningUIForTowerNumber();
            if (!IsPreTowerExist && IsAbleToSet[2])
            {
                PreTower = Instantiate(PreChainTowerprefab, target, Quaternion.identity);
                PreTower.transform.parent = player.transform;
                IsPreTowerExist = true;
                TowerIndex = 3;
            }

        }

        if (InputManager.Instance.GetKeyDown("PreBuildLightingTower"))
        {
            ShowWarningUIForTowerNumber();
            if (!IsPreTowerExist && IsAbleToSet[3])
            {
                PreTower = Instantiate(PreLightingTowerprefab, target, Quaternion.identity);
                PreTower.transform.parent = player.transform;
                IsPreTowerExist = true;
                TowerIndex = 4;
            }

        }

        if (InputManager.Instance.GetKeyDown("PreBuildTauntTower"))
        {
            ShowWarningUIForTowerNumber();
            if (!IsPreTowerExist && IsAbleToSet[4])
            {
                PreTower = Instantiate(PreTauntTowerprefab, target, Quaternion.identity);
                PreTower.transform.parent = player.transform;
                IsPreTowerExist = true;
                TowerIndex = 5;
            }

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
                    if (TowerIndex == 4)
                    {
                        Instantiate(LightingTowerprefab, target, Quaternion.identity);
                        player.GetComponent<Player>().coin -= 200;
                    }
                    if (TowerIndex == 5)
                    {
                        Instantiate(TauntTowerprefab, target, Quaternion.identity);
                        player.GetComponent<Player>().coin -= 250;
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
    void ApplyUnlock()
    {
        if (TowerNumber >= TowerNumberLimit)
        {
            Tower1Cover.fillAmount = 0.0f;
            Tower2Cover.fillAmount = 0.0f;
            Tower3Cover.fillAmount = 0.0f;
            Tower4Cover.fillAmount = 0.0f;
            Tower5Cover.fillAmount = 0.0f;
            return;
        }

        if (player.GetComponent<Player>().coin >= TowerTemplates[0].price)
        {
            Tower1Cover.fillAmount = 1.0f;
        }
        if (player.GetComponent<Player>().coin >= TowerTemplates[1].price)
        {
            Tower2Cover.fillAmount = 1.0f;
        }
        if (player.GetComponent<Player>().coin >= TowerTemplates[2].price)
        {
            Tower3Cover.fillAmount = 1.0f;
        }
        if (player.GetComponent<Player>().coin >= TowerTemplates[3].price)
        {
            Tower4Cover.fillAmount = 1.0f;
        }
        if (player.GetComponent<Player>().coin >= TowerTemplates[4].price)
        {
            Tower5Cover.fillAmount = 1.0f;
        }

        else
        {
            Tower1Cover.fillAmount = (float)player.GetComponent<Player>().coin / (float)TowerTemplates[0].price;
            Tower2Cover.fillAmount = (float)player.GetComponent<Player>().coin / (float)TowerTemplates[1].price;
            Tower3Cover.fillAmount = (float)player.GetComponent<Player>().coin / (float)TowerTemplates[2].price;
            Tower4Cover.fillAmount = (float)player.GetComponent<Player>().coin / (float)TowerTemplates[3].price;
            Tower5Cover.fillAmount = (float)player.GetComponent<Player>().coin / (float)TowerTemplates[4].price;
        }




    }
    // Update is called once per frame
    void Update()
    {
        CheckSelection();
        //ChangePanel();
        //Coinnumber.text = player.GetComponent<Player>().coin.ToString();
        TowerNumberText.text = SetTowerNumberUI(TowerNumber, TowerNumberLimit).ToString();
        CheckNumberLimit();
        
        ApplyUnlock();
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
