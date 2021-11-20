using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    private Indicator indicator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (QuestSystem.Instance.MoveComplete  &&
            QuestSystem.Instance.ShootComplete &&
            QuestSystem.Instance.BuildComplete &&
            QuestSystem.Instance.TrapComplete)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene("Level1");
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        indicator = gameObject.GetComponentInChildren<Indicator>();
        indicator.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (QuestSystem.Instance.MoveComplete &&
            QuestSystem.Instance.ShootComplete &&
            QuestSystem.Instance.BuildComplete &&
            QuestSystem.Instance.TrapComplete)
        {
            indicator.gameObject.SetActive(true);
        }
    }
}
