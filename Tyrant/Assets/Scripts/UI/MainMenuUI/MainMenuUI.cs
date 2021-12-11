using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private GameObject settingMenu;
    [SerializeField]
    private GameObject modeSelect;
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
        modeSelect.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void onClickSetting()
    {
        settingMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    public void onClickQuit()
    {
        Application.Quit();
    }
}
