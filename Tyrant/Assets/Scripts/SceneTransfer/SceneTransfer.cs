using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(QuestSystem.Instance.MoveComplete &&
            QuestSystem.Instance.ShootComplete &&
            QuestSystem.Instance.BuildComplete &&
            QuestSystem.Instance.TrapComplete )
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene("Level1");
            }
        }
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
