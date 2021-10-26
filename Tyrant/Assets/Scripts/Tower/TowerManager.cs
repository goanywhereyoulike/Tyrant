using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{


    public List<GameObject> slots = new List<GameObject>();

    public TowerLimitTemplate towerroomInfos;
    PlayerMovement player;
    SpriteRenderer TowerSprite;
    public List<TowerTemplate> TowerTemplates = new List<TowerTemplate>();

    TowerSlot[] allslots;

    [SerializeField]
    GameObject TowerSlots;

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
        RoomManager.Instance.RoomChanged += RoomChange;
        TowerPanel.gameObject.SetActive(true);
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

        TowerNumberLimit = towerroomInfos.towerroomInfo[0].TowerNumber;
        //TowerNumlimit.text = TowerNumberLimit.ToString();
        TowerNumberText.text = SetTowerNumberUI(TowerNumber, TowerNumberLimit).ToString();

        allslots = TowerSlots.GetComponentsInChildren<TowerSlot>();
        //towerroomInfos.towerroomInfo.Reverse(RoomManager.Instance.);
    }

    void UpdateUI()
    {
        for (int i = 0; i < 5; ++i)
        {
            TowerSlot slot = slots[i].GetComponent<TowerSlot>();
            if (!SelectAbleToSet[i])
            {
                slot.IsShow = false;
                slot.gameObject.transform.SetAsLastSibling();

            }
            else
            {
                slot.IsShow = true;

            }


        }

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

    private void RoomChange(int changeId)
    {
        ClearAll();
        roomNumber = changeId;
        TowerNumberLimit = towerroomInfos.towerroomInfo[roomNumber].TowerNumber;
        TowerNumberText.text = SetTowerNumberUI(TowerNumber, TowerNumberLimit).ToString();
        CheckSelection();
        UpdateUI();
    }

    public void ResetTowerNumber()
    {
        TowerNumber = 0;
        IsReachTowerNumberLimit = false;
        for (int i = 0; i < 5; ++i)
        {
            slots[i].gameObject.transform.SetSiblingIndex(i);

            slots[i].GetComponent<TowerSlot>().IsShow = true;
        }

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
                slots[i].GetComponent<TowerSlot>().LockUI();

            }

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

        for (int i = 0; i < 5; ++i)
        {
            IsAbleToSet[i] = SelectAbleToSet[i];

        }
        //IsReachTowerNumberLimit = true;
    }
    //void CheckCoin()
    //{
    //    if (IsReachTowerNumberLimit)
    //    {
    //        return;
    //    }

    //    if (player.GetComponent<Player>().coin < TowerTemplates[0].price)
    //    {

    //        IsAbleToSet[0] = false;
    //    }
    //    if (player.GetComponent<Player>().coin < TowerTemplates[1].price)
    //    {

    //        IsAbleToSet[1] = false;
    //    }
    //    if (player.GetComponent<Player>().coin < TowerTemplates[2].price)
    //    {

    //        IsAbleToSet[2] = false;
    //    }
    //    if (player.GetComponent<Player>().coin < TowerTemplates[3].price)
    //    {

    //        IsAbleToSet[3] = false;
    //    }
    //    if (player.GetComponent<Player>().coin < TowerTemplates[4].price)
    //    {

    //        IsAbleToSet[4] = false;
    //    }
    //    if (player.GetComponent<Player>().coin >= TowerTemplates[0].price && SelectAbleToSet[0])
    //    {

    //        IsAbleToSet[0] = true;
    //    }
    //    if (player.GetComponent<Player>().coin >= TowerTemplates[1].price && SelectAbleToSet[1])
    //    {

    //        IsAbleToSet[1] = true;
    //    }
    //    if (player.GetComponent<Player>().coin >= TowerTemplates[2].price && SelectAbleToSet[2])
    //    {

    //        IsAbleToSet[2] = true;
    //    }
    //    if (player.GetComponent<Player>().coin >= TowerTemplates[3].price && SelectAbleToSet[3])
    //    {

    //        IsAbleToSet[3] = true;
    //    }
    //    if (player.GetComponent<Player>().coin >= TowerTemplates[4].price && SelectAbleToSet[4])
    //    {

    //        IsAbleToSet[4] = true;
    //    }

    //}


    void SetTower(Vector2 target)
    {
        if (IsReachTowerNumberLimit)
        {
            return;
        }

        allslots = TowerSlots.GetComponentsInChildren<TowerSlot>();
        if (InputManager.Instance.GetKeyDown("PreBuildTower"))
        {

            ShowWarningUIForTowerNumber();
            if (!IsPreTowerExist && allslots[0].IsShow)
            {
                PreTower = Instantiate(allslots[0].PreTowerprefab, target, Quaternion.identity);
                PreTower.transform.parent = player.transform;
                IsPreTowerExist = true;
                TowerIndex = 0;
            }
        }

        if (InputManager.Instance.GetKeyDown("PreBuildCannonTower"))
        {
            ShowWarningUIForTowerNumber();
            if (!IsPreTowerExist && allslots[1].IsShow)
            {
                PreTower = Instantiate(allslots[1].PreTowerprefab, target, Quaternion.identity);
                PreTower.transform.parent = player.transform;
                IsPreTowerExist = true;
                TowerIndex = 1;
            }

        }

        if (InputManager.Instance.GetKeyDown("PreBuildChainTower"))
        {
            ShowWarningUIForTowerNumber();
            if (!IsPreTowerExist && allslots[2].IsShow)
            {
                PreTower = Instantiate(allslots[2].PreTowerprefab, target, Quaternion.identity);
                PreTower.transform.parent = player.transform;
                IsPreTowerExist = true;
                TowerIndex = 2;
            }

        }

        if (InputManager.Instance.GetKeyDown("PreBuildLightingTower"))
        {
            ShowWarningUIForTowerNumber();
            if (!IsPreTowerExist && allslots[3].IsShow)
            {
                PreTower = Instantiate(allslots[3].PreTowerprefab, target, Quaternion.identity);
                PreTower.transform.parent = player.transform;
                IsPreTowerExist = true;
                TowerIndex = 3;
            }

        }

        if (InputManager.Instance.GetKeyDown("PreBuildTauntTower"))
        {
            ShowWarningUIForTowerNumber();
            if (!IsPreTowerExist && allslots[4].IsShow)
            {
                PreTower = Instantiate(allslots[4].PreTowerprefab, target, Quaternion.identity);
                PreTower.transform.parent = player.transform;
                IsPreTowerExist = true;
                TowerIndex = 4;
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
                    Instantiate(allslots[TowerIndex].Towerprefab, target, Quaternion.identity);
                    player.GetComponent<Player>().coin -= allslots[TowerIndex].towerTemplate.price;

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
            foreach (var slot in slots)
            {
                slot.GetComponent<TowerSlot>().SetFill(0.0f);

            }
            return;
        }

        for (int i = 0; i < slots.Count; ++i)
        {
            TowerSlot slot = slots[i].GetComponent<TowerSlot>();
            if (slot.IsShow)
            {
                slot.CheckCoin();

            }

        }

    }
    // Update is called once per frame
    void Update()
    {
        CheckSelection();
        UpdateUI();
        //ChangePanel();
        //Coinnumber.text = player.GetComponent<Player>().coin.ToString();
        TowerNumberText.text = SetTowerNumberUI(TowerNumber, TowerNumberLimit).ToString();
        CheckNumberLimit();

        ApplyUnlock();
        //CheckCoin();
        Vector2 PlayerPos = player.transform.position + offset;
        SetTower(PlayerPos);

        if (PreTower)
        {
            PreTower.transform.position = PlayerPos;
        }
        DestroyTower();
    }
}
