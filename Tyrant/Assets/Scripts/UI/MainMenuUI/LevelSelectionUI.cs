using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSelectionUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Button[] levelButtons;

    private void Start()
    {
        for(int i =0; i<levelButtons.Length;++i)
        {
            int x = i;
            levelButtons[i].onClick.AddListener(() => levelButtonClicked(x));
        }
    }

    void levelButtonClicked(int index)
    {
        SceneManager.LoadScene(index+1);
    }
}
