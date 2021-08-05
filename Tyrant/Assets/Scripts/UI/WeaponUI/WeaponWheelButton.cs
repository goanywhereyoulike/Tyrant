using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponWheelButton : MonoBehaviour
{
    [SerializeField]
    private WeaponWheelController weaponWheelController = null;
    public int id;
    private Animator animator;
    public string itemName;
    public TextMeshProUGUI itemText;
    public Image selectItem;
    private Color color = new Color(1f, 1f, 1f, 1f);
    private bool selected = false;
    public Sprite icon;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        selectItem.sprite = null;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(selected&& weaponWheelController.WeaponID != 0)
        {
            color.a = 1f;
            selectItem.color = color;
            selectItem.sprite = icon;
            itemText.text = itemName;
        }
        if (weaponWheelController.WeaponID ==0)
        {
            color.a = 0f;
            selectItem.color=  color;
        }

    }
    public void Selected()
    {
        selected = true;
        Debug.Log("Select  " + selected);
        weaponWheelController.WeaponID = id;
        weaponWheelController.WeaponWheelSelect = false;
    }
    public void DeSelect()
    {
        selected = false;
        Debug.Log("DeSelect  "+selected);
        //weaponWheelController.WeaponID = 0;
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
