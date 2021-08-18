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
    private Text Trap1Price;

    [SerializeField]
    private Text Trap2Price;

    [SerializeField]
    private Text Trap3Price;

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

    private GameObject PreTrap;

    public Vector3 offset;


    private int TrapNumber = 0;
    List<bool> IsAbleToSet = new List<bool>(3);
    GameObject preTrap;
    bool IsPreTrapExist = false;
    // Start is called before the first frame update
    void Start()
    {
        //TrapPanel.gameObject.SetActive(true);
        player = FindObjectOfType<PlayerMovement>();
        for (int i = 0; i < 3; i++)
        {
            IsAbleToSet.Add(false);
        }

        Trap1Price.text = Traps[0].price.ToString();
        Trap2Price.text = Traps[1].price.ToString();
        Trap3Price.text = Traps[2].price.ToString();

        //GetComponentInChildren<CanvasGroup>().alpha = 0;
        //GetComponentInChildren<CanvasGroup>().interactable = false;
        //GetComponentInChildren<CanvasGroup>().blocksRaycasts = false;

    }

    void DestroyTower()
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

    void CheckCoin()
    {

        if (player.GetComponent<Player>().coin < Traps[0].price)
        {
            PreClipSprite.color = Color.red;
            IsAbleToSet[0] = false;
        }
        if (player.GetComponent<Player>().coin < Traps[1].price)
        {
            PreBombSprite.color = Color.red;
            IsAbleToSet[1] = false;
        }
        if (player.GetComponent<Player>().coin < Traps[2].price)
        {
            PreBlackHoleSprite.color = Color.red;
            IsAbleToSet[2] = false;
        }
        if (player.GetComponent<Player>().coin >= Traps[0].price)
        {
            PreClipSprite.color = Color.green;
            IsAbleToSet[0] = true;
        }
        if (player.GetComponent<Player>().coin >= Traps[1].price)
        {
            PreBombSprite.color = Color.green;
            IsAbleToSet[1] = true;
        }
        if (player.GetComponent<Player>().coin >= Traps[2].price)
        {
            PreBlackHoleSprite.color = Color.green;
            IsAbleToSet[2] = true;
        }



    }


    void SetTower(Vector2 target)
    {

        if (InputManager.Instance.GetKeyDown("PreBuildTower") && !IsPreTrapExist && IsAbleToSet[0])
        {

            PreTrap = Instantiate(PreClipprefab, target, Quaternion.identity);
            PreTrap.transform.parent = player.transform;
            IsPreTrapExist = true;
            TrapNumber = 1;

        }

        if (InputManager.Instance.GetKeyDown("PreBuildCannonTower") && !IsPreTrapExist && IsAbleToSet[1])
        {

            PreTrap = Instantiate(PreBombprefab, target, Quaternion.identity);
            PreTrap.transform.parent = player.transform;
            IsPreTrapExist = true;
            TrapNumber = 2;

        }

        if (InputManager.Instance.GetKeyDown("PreBuildChainTower") && !IsPreTrapExist && IsAbleToSet[2])
        {

            PreTrap = Instantiate(PreBlackHoleprefab, target, Quaternion.identity);
            PreTrap.transform.parent = player.transform;
            IsPreTrapExist = true;
            TrapNumber = 3;

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
                    if (TrapNumber == 1)
                    {
                        Instantiate(Clipprefab, target, Quaternion.identity);
                        player.GetComponent<Player>().coin -= 50;
                    }
                    if (TrapNumber == 2)
                    {
                        Instantiate(Bombprefab, target, Quaternion.identity);
                        player.GetComponent<Player>().coin -= 100;
                    }
                    if (TrapNumber == 3)
                    {
                        Instantiate(BlackHoleprefab, target, Quaternion.identity);
                        player.GetComponent<Player>().coin -= 150;
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

        CheckCoin();
        Vector2 PlayerPos = player.transform.position + offset;
        SetTower(PlayerPos);

        if (PreTrap)
        {
            PreTrap.transform.position = PlayerPos;
        }
        DestroyTower();
    }
}
