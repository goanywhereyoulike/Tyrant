using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelectUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onCasualClicked()
    {
        GameSetting.Instance.isDemoMode = true;
        SceneManager.LoadScene("TutorialScene");
    }
    public void onHardCoreClicked()
    {
        GameSetting.Instance.isDemoMode = false;
        SceneManager.LoadScene("TutorialScene");
    }
}
