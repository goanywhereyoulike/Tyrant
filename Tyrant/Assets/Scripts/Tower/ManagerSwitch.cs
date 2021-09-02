using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerSwitch : MonoBehaviour
{
    PlayerMovement player;

    [SerializeField]
    GameObject TrapManager;

    TowerManager towermng;
    TrapManager trapmgn;

    [SerializeField]
    private Text Coinnumber;

    private void Start()
    {
        towermng = FindObjectOfType<TowerManager>();
        trapmgn = TrapManager.GetComponent<TrapManager>();
        player = FindObjectOfType<PlayerMovement>();

        towermng.gameObject.SetActive(true);
    }
    void Update()
    {

        ChangePanel();
        Coinnumber.text = player.GetComponent<Player>().coin.ToString();
    }

    void ChangePanel()
    {

        if (InputManager.Instance.GetKeyDown("ChangeTowerPanel"))
        {
            if (towermng.gameObject.activeSelf == true && !towermng.IsPreTowerExist)
            {
                towermng.gameObject.SetActive(false);
                trapmgn.gameObject.SetActive(true);
                //trapmgn.gameObject.GetComponentInChildren<CanvasGroup>().alpha = 1;
                //trapmgn.gameObject.GetComponentInChildren<CanvasGroup>().interactable = true;
                //trapmgn.gameObject.GetComponentInChildren<CanvasGroup>().blocksRaycasts = true;
            }
            else if (trapmgn.gameObject.activeSelf == true && !trapmgn.IsPreTrapExist)
            {
                towermng.gameObject.SetActive(true);
                trapmgn.gameObject.SetActive(false);
            }
        }



    }



}
