using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerSlot : MonoBehaviour
{
    // Start is called before the first frame update
    Player player;

    private bool Isshow = true;
    public bool IsShow { get => Isshow; set => Isshow = value; }

    bool IsAbleToSet = false;


    public GameObject PreTowerprefab;

    public GameObject Towerprefab;


    public TowerTemplate towerTemplate;

    [SerializeField]
    private TMP_Text TowerPrice;

    [SerializeField]
    private Image TowerCover;

    [SerializeField]
    private Image TowerImage;



    void Start()
    {
        TowerPrice.text = towerTemplate.price.ToString();
        player = FindObjectOfType<Player>();
    }

    public void LockUI()
    {
        TowerCover.fillAmount = 0.0f;

    }

    public void SetFill(float n)
    {

        TowerCover.fillAmount = n;


    }

    public void CheckCoin()
    {

        if (player.coin >= towerTemplate.price)
        {
            TowerCover.fillAmount = 1.0f;
        }
        else
        {
            TowerCover.fillAmount = (float)player.GetComponent<Player>().coin / (float)towerTemplate.price;
        }




    }

    public void UnlockUI()
    {

        TowerCover.color = Color.white;
        TowerImage.color = Color.white;

    }


    void UpdateUI()
    {

        if (Isshow)
        {
            TowerPrice.gameObject.SetActive(true);
            TowerCover.gameObject.SetActive(true);
            TowerImage.gameObject.SetActive(true);

        }
        else
        {
            TowerPrice.gameObject.SetActive(false);
            TowerCover.gameObject.SetActive(false);
            TowerImage.gameObject.SetActive(false);

        }

    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }
}
