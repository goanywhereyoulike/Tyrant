using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField]
    private GameObject PauseMenu;
    [SerializeField]
    private GameObject SettingMenu;
    [SerializeField]
    private GameObject DarkBackGround;
    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.GetKey("Esc") && !isPaused)
        {
            PauseMenu.SetActive(true);
            DarkBackGround.SetActive(true);
            Time.timeScale = 0.0f;
            isPaused = true;
        }
    }

    public void onResumeClicked()
    {
        PauseMenu.SetActive(false);
        DarkBackGround.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }
    public void onRestartClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
        isPaused = false;
    }
    public void onSettingClicked()
    {
        SettingMenu.SetActive(true);
        PauseMenu.SetActive(false);
    }
    public void onQuitClicked()
    {
        Application.Quit();
    }
}
