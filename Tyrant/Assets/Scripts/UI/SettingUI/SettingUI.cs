using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField]
    private KeyBindUI keyBindUIPrefab;
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<InputManager.Instance.KeyCodeDict.Count;++i)
        {
            KeyBindUI key=Instantiate(keyBindUIPrefab);
            key.keycode.GetComponentInChildren<Text>().text = InputManager.Instance.KeyCodeDict.ElementAt(i).Value.ToString();
            key.keyName.text = InputManager.Instance.KeyCodeDict.ElementAt(i).Key.ToString();
            key.transform.SetParent(content.transform);
        }
        
    }

    public void OnReturnClicked()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
