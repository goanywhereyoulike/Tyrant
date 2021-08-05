using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private GameObject settingMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onCLickStart()
    {
        SceneManager.LoadScene("TPS1");
    }
    public void onClickSetting()
    {
        settingMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
