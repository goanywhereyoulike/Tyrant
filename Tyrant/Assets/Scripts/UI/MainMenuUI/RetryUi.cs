using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryUi : MonoBehaviour
{
    [SerializeField]
    private Player player;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onRetryClicked()
    {
        player.ResetHealth();
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    public void onQuitClicked()
    {
        Application.Quit();
    }
}
