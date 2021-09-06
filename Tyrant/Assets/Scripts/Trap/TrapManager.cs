using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapManager : MonoBehaviour
{
    PlayerMovement player;
    SpriteRenderer TrapSprite;
    public List<TrapTemplate> Traps = new List<TrapTemplate>();

    [SerializeField]
    private Image TrapPanel;

    //[SerializeField]
    //private Text Coinnumber;

    [SerializeField]
    private Text Trap1number;

    [SerializeField]
    private Text Trap2number;

    [SerializeField]
    private Text Trap3number;

    [SerializeField]
    Image PreClipSprite;

    [SerializeField]
    Image PreBombSprite;

    [SerializeField]
    Image PreBlackHoleSprite;

    public GameObject Clipprefab;
    public GameObject Bombprefab;
    public GameObject BlackHoleprefab;

    public GameObject PreClipprefab;
    public GameObject PreBombprefab;
    public GameObject PreBlackHoleprefab;
                      
    private int Clipnumber      = 0;
    private int Bombnumber      = 0;
    private int BlckHolenumber  = 0;


    private GameObject PreTrap;

    public Vector3 offset;

    private int TrapIndex = -1;
    //private int TotalTrapNumber = 0;
    List<bool> IsAbleToSet = new List<bool>(3);
    GameObject preTrap;
    public bool IsPreTrapExist = false;
    // Start is called before the first frame update
    void Start()
    {
        //TrapPanel.gameObject.SetActive(true);
        player = FindObjectOfType<PlayerMovement>();
        for (int i = 0; i < 3; i++)
        {
            IsAbleToSet.Add(false);
        }

        Trap1number.text = Clipnumber.ToString();
        Trap2number.text = Bombnumber.ToString();
        Trap3number.text = BlckHolenumber.ToString();

        //GetComponentInChildren<CanvasGroup>().alpha = 0;
        //GetComponentInChildren<CanvasGroup>().interactable = false;
        //GetComponentInChildren<CanvasGroup>().blocksRaycasts = false;

    }

    void DestroyTrap()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (InputManager.Instance.GetKeyDown("DestroyTower"))
            {
                if (hit.collider.gameObject.tag == "Trap")
                {
                    Destroy(hit.collider.gameObject.transform.parent.gameObject);

                }
            }


        }

    }
    public void AddClipTrap()
    {
        Clipnumber++;
    }
    public void AddBombTrap()
    {
        Bombnumber++;
    }
    public void AddBlackHoleTrap()
    {
        BlckHolenumber++;
    }

    void CheckTrapnumber()
    {
        if (Clipnumber <= 0)
        {
            PreClipSprite.color = Color.red;
            IsAbleToSet[0] = false;
            Clipnumber = 0;

        }
        else
        {
            PreClipSprite.color = Color.green;
            IsAbleToSet[0] = true;

        }
        if (Bombnumber <= 0)
        {
            PreBombSprite.color = Color.red;
            IsAbleToSet[1] = false;
            Bombnumber = 0;

        }
        else 
        {
            PreBombSprite.color = Color.green;
            IsAbleToSet[1] = true;

        }
        if (BlckHolenumber <= 0)
        {
            PreBlackHoleSprite.color = Color.red;
            IsAbleToSet[2] = false;
            BlckHolenumber = 0;
        }
        else 
        {
            PreBlackHoleSprite.color = Color.green;
            IsAbleToSet[2] = true;

        }

    }

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

    void UpdateTrapUI()
    {
        Trap1number.text = Clipnumber.ToString();
        Trap2number.text = Bombnumber.ToString();
        Trap3number.text = BlckHolenumber.ToString();

    }

    void SetTrap(Vector2 target)
    {

        if (InputManager.Instance.GetKeyDown("PreBuildTower") && !IsPreTrapExist && IsAbleToSet[0])
        {

            PreTrap = Instantiate(PreClipprefab, target, Quaternion.identity);
            PreTrap.transform.parent = player.transform;
            IsPreTrapExist = true;
            TrapIndex = 1;


        }

        if (InputManager.Instance.GetKeyDown("PreBuildCannonTower") && !IsPreTrapExist && IsAbleToSet[1])
        {

            PreTrap = Instantiate(PreBombprefab, target, Quaternion.identity);
            PreTrap.transform.parent = player.transform;
            IsPreTrapExist = true;
            TrapIndex = 2;

        }

        if (InputManager.Instance.GetKeyDown("PreBuildChainTower") && !IsPreTrapExist && IsAbleToSet[2])
        {

            PreTrap = Instantiate(PreBlackHoleprefab, target, Quaternion.identity);
            PreTrap.transform.parent = player.transform;
            IsPreTrapExist = true;
            TrapIndex = 3;

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
                        Clipnumber--;
                    }
                    if (TrapIndex == 2)
                    {
                        Instantiate(Bombprefab, target, Quaternion.identity);
                        Bombnumber--;
                    }
                    if (TrapIndex == 3)
                    {
                        Instantiate(BlackHoleprefab, target, Quaternion.identity);
                        BlckHolenumber--;
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

        // ChangePanel();
        //Coinnumber.text = player.GetComponent<Player>().coin.ToString();
        //CheckLock();
        //CheckCoin();
        UpdateTrapUI();
        CheckTrapnumber();
        Vector2 PlayerPos = player.transform.position + offset;
        SetTrap(PlayerPos);

        if (PreTrap)
        {
            PreTrap.transform.position = PlayerPos;
        }
       
        DestroyTrap();
    }
}
