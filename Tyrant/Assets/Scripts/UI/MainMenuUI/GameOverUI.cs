using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = true;
    }
    public void onReturnClicked()
    {
        SceneManager.LoadScene("MenuUI");
    }
    public void onQuitClicked()
    {
        Application.Quit();
    }
    public void onRetryClicked()
    {
        SceneManager.LoadScene("Level1");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
