using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrapManager : MonoBehaviour
{
    PlayerMovement player;
    SpriteRenderer TrapSprite;
    [SerializeField]
    TrapLimitTemplate traptemplate;
    int LevelNumber = 0;
    int RoomNumber = 0;
    TrapRoomInfo.TrapType trapType;

    [SerializeField]
    List<Image> TrapImages = new List<Image>();


    [SerializeField]
    Image ClipTrapImage;

    [SerializeField]
    Image BombTrapImage;

    [SerializeField]
    Image BlackHoleTrapImage;

    [SerializeField]
    private Image TrapPanel;

    [SerializeField]
    private TMP_Text Trapnumber;

    //[SerializeField]
    //private Text Coinnumber;

    //[SerializeField]
    //private Text Trap1number;

    //[SerializeField]
    //private Text Trap2number;

    //[SerializeField]
    //private Text Trap3number;

    //[SerializeField]
    //Image PreClipSprite;

    //[SerializeField]
    //Image PreBombSprite;

    //[SerializeField]
    //Image PreBlackHoleSprite;

    public GameObject Clipprefab;
    public GameObject Bombprefab;
    public GameObject BlackHoleprefab;

    public GameObject PreClipprefab;
    public GameObject PreBombprefab;
    public GameObject PreBlackHoleprefab;

    //private int Clipnumber      = 0;
    //private int Bombnumber      = 0;
    //private int BlckHolenumber  = 0;

    private int TrapNumber = 0;


    private GameObject PreTrap;

    public Vector3 offset;

    private int TrapIndex = -1;
    //private int TotalTrapNumber = 0;
    bool IsAbleToSet = true;
    GameObject preTrap;
    public bool IsPreTrapExist = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        Trapnumber.text = TrapNumber.ToString();
        RoomManager.Instance.RoomChanged += RoomChanged;
        LevelNumber = SceneManager.GetActiveScene().buildIndex;
        //if (traptemplate.traproominfo[LevelNumber - 1].level == LevelNumber)
        //{
        //    trapType = traptemplate.traproominfo[LevelNumber - 1].trap;
        //}
        if (traptemplate.traproominfo[RoomNumber].level == LevelNumber)
        {
            trapType = traptemplate.traproominfo[RoomNumber].trap;
        }
        SetTrapImage(trapType);

        //DontDestroyOnLoad(this);
        //Trap1number.text = Clipnumber.ToString();
        //Trap2number.text = Bombnumber.ToString();
        //Trap3number.text = BlckHolenumber.ToString();
    }

    //void DestroyTrap()
    //{
    //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

    //    if (hit.collider != null)
    //    {
    //        if (InputManager.Instance.GetKeyDown("DestroyTower"))
    //        {
    //            if (hit.collider.gameObject.tag == "Trap")
    //            {
    //                Destroy(hit.collider.gameObject.transform.parent.gameObject);

    //            }
    //        }


    //    }

    //}

    //public void AddClipTrap()
    //{
    //    Clipnumber++;
    //}
    //public void AddBombTrap()
    //{
    //    Bombnumber++;
    //}
    //public void AddBlackHoleTrap()
    //{
    //    BlckHolenumber++;
    //}

    //void CheckTrapnumber()
    //{
    //    if (Clipnumber <= 0)
    //    {
    //        PreClipSprite.color = Color.red;
    //        IsAbleToSet[0] = false;
    //        Clipnumber = 0;

    //    }
    //    else
    //    {
    //        PreClipSprite.color = Color.green;
    //        IsAbleToSet[0] = true;

    //    }
    //    if (Bombnumber <= 0)
    //    {
    //        PreBombSprite.color = Color.red;
    //        IsAbleToSet[1] = false;
    //        Bombnumber = 0;

    //    }
    //    else
    //    {
    //        PreBombSprite.color = Color.green;
    //        IsAbleToSet[1] = true;

    //    }
    //    if (BlckHolenumber <= 0)
    //    {
    //        PreBlackHoleSprite.color = Color.red;
    //        IsAbleToSet[2] = false;
    //        BlckHolenumber = 0;
    //    }
    //    else
    //    {
    //        PreBlackHoleSprite.color = Color.green;
    //        IsAbleToSet[2] = true;

    //    }

    //}

    //void CheckCoin()
    //{

    //    if (player.GetComponent<Player>().coin < Traps[0].price && IsClipUnlock)
    //    {
    //        PreClipSprite.color = Color.red;
    //        IsAbleToSet[0] = false;
    //    }
    //    if (player.GetComponent<Player>().coin < Traps[1].price && IsBombUnlock)
    //    {
    //        PreBombSprite.color = Color.red;
    //        IsAbleToSet[1] = false;
    //    }
    //    if (player.GetComponent<Player>().coin < Traps[2].price && IsBlckHoleUnlock)
    //    {
    //        PreBlackHoleSprite.color = Color.red;
    //        IsAbleToSet[2] = false;
    //    }
    //    if (player.GetComponent<Player>().coin >= Traps[0].price && IsClipUnlock)
    //    {
    //        PreClipSprite.color = Color.green;
    //        IsAbleToSet[0] = true;
    //    }
    //    if (player.GetComponent<Player>().coin >= Traps[1].price && IsBombUnlock)
    //    {
    //        PreBombSprite.color = Color.green;
    //        IsAbleToSet[1] = true;
    //    }
    //    if (player.GetComponent<Player>().coin >= Traps[2].price && IsBlckHoleUnlock)
    //    {
    //        PreBlackHoleSprite.color = Color.green;
    //        IsAbleToSet[2] = true;
    //    }



    //}


    void SetTrapImage(TrapRoomInfo.TrapType trapType)
    {
        foreach (var trapImage in TrapImages)
        {
            trapImage.gameObject.SetActive(false);
        }
        if (trapType == TrapRoomInfo.TrapType.Clip)
        {
            ClipTrapImage.gameObject.SetActive(true);

        }
        else if (trapType == TrapRoomInfo.TrapType.Bomb)
        {
            BombTrapImage.gameObject.SetActive(true);
        }
        else if (trapType == TrapRoomInfo.TrapType.BlackHole)
        {
            BlackHoleTrapImage.gameObject.SetActive(true);
        }
    }

    private void RoomChanged(int changeId)
    {
        ClearAll();
        RoomNumber = changeId;
        trapType = traptemplate.traproominfo[RoomNumber].trap;

        SetTrapImage(trapType);


    }
    void ClearAll()
    {
        TrapNumber = 0;
        Trapnumber.text = TrapNumber.ToString();
    }
    public void AddTrap()
    {
        TrapNumber++;
        Trapnumber.text = TrapNumber.ToString();
    }

    void CheckTrapnumber()
    {
        if (TrapNumber <= 0)
        {
            //Slot.GetComponent<SpriteRenderer>().color = Color.red;
            TrapNumber = 0;
            IsAbleToSet = false;
            TrapPanel.gameObject.SetActive(false);

        }
        else
        {
            //Slot.GetComponent<SpriteRenderer>().color = Color.green;
            TrapPanel.gameObject.SetActive(true);
            IsAbleToSet = true;
        }


    }


    void SetTrap(Vector2 target)
    {
        if (InputManager.Instance.GetKeyDown("PreBuildTrap"))
        {
            if (trapType == TrapRoomInfo.TrapType.Clip && !IsPreTrapExist && IsAbleToSet)
            {

                PreTrap = Instantiate(PreClipprefab, target, Quaternion.identity);
                PreTrap.transform.parent = player.transform;
                IsPreTrapExist = true;
                TrapIndex = 1;


            }

            if (trapType == TrapRoomInfo.TrapType.Bomb && !IsPreTrapExist && IsAbleToSet)
            {

                PreTrap = Instantiate(PreBombprefab, target, Quaternion.identity);
                PreTrap.transform.parent = player.transform;
                IsPreTrapExist = true;
                TrapIndex = 2;

            }

            if (trapType == TrapRoomInfo.TrapType.BlackHole && !IsPreTrapExist && IsAbleToSet)
            {

                PreTrap = Instantiate(PreBlackHoleprefab, target, Quaternion.identity);
                PreTrap.transform.parent = player.transform;
                IsPreTrapExist = true;
                TrapIndex = 3;

            }
        }
        if (InputManager.Instance.GetKeyDown("dropTower") && IsPreTrapExist)
        {
            BluePrint blue = player.GetComponentInChildren<BluePrint>();
            if (blue)
            {
                Destroy(blue.gameObject);
            }
            IsPreTrapExist = false;

        }

        if (PreTrap)
        {
            BluePrint bluePrint = PreTrap.GetComponent<BluePrint>();
            if (bluePrint)
            {
                TrapSprite = bluePrint.gameObject.GetComponent<SpriteRenderer>();
            }


            if (bluePrint && bluePrint.IsAbleToSet)
            {
                TrapSprite.color = Color.green;
                if (InputManager.Instance.GetKeyDown("BuildTower"))
                {
                    if (TrapIndex == 1)
                    {
                        Instantiate(Clipprefab, target, Quaternion.identity);
                        TrapNumber--;
                        Trapnumber.text = TrapNumber.ToString();
                    }
                    if (TrapIndex == 2)
                    {
                        Instantiate(Bombprefab, target, Quaternion.identity);
                        TrapNumber--;
                        Trapnumber.text = TrapNumber.ToString();
                    }
                    if (TrapIndex == 3)
                    {
                        Instantiate(BlackHoleprefab, target, Quaternion.identity);
                        TrapNumber--;
                        Trapnumber.text = TrapNumber.ToString();
                    }

                    Destroy(bluePrint.gameObject);
                    IsPreTrapExist = false;
                }
            }
            else if (bluePrint && !bluePrint.IsAbleToSet)
            {
                TrapSprite.color = Color.red;

            }
        }


    }
    // Update is called once per frame
    void Update()
    {

        CheckTrapnumber();
        Vector2 PlayerPos = player.transform.position + offset;
        SetTrap(PlayerPos);
        SetTrapImage(trapType);
        if (PreTrap)
        {
            PreTrap.transform.position = PlayerPos;
        }
    }
}
