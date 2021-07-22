using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponWheelButton : MonoBehaviour
{
    public int id;
    private Animator animator;
    public string itemName;
    public TextMeshProUGUI itemText;
    public Image selectItem;
    private bool selected = false;
    public Sprite icon;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(selected)
        {
            selectItem.sprite = icon;
            itemText.text = itemName;
        }
    }
    public void Selected()
    {
        selected = true;
        WeaponWheelController.weaponID = id;
    }
    public void DeSelect()
    {
        selected = false;
        WeaponWheelController.weaponID = 0;
    }
    public void HoverEnter()
    {
        animator.SetBool("Hover", true);
        itemText.text = itemName;
    }
    public void HoverExit()
    {
        animator.SetBool("Hover", false);
        itemText.text = "";
    }
}
