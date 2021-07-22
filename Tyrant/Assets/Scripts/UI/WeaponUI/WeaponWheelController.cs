using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public Animator animator;
    private bool weaponWheelSelect = false;
    public Image selectItem;
    public Sprite noImage;
    public static int weaponID;

    void Update()
    {

        if (InputManager.Instance.GetKeyDown("WeaponWheelToggle"))
        {
            weaponWheelSelect = !weaponWheelSelect;
        }
        if (weaponWheelSelect)
        {
            animator.SetBool("OpenWeaponWheel", true);
        }
        else
        {
            animator.SetBool("OpenWeaponWheel", false);
        }

        switch(weaponID)//select nothing
        {
            case 0:
                selectItem.sprite = null ;
                break;
            case 1:
                Debug.Log("gun1"); //switch weapon here
                break;
            case 2:
                Debug.Log("gun2");
                break;
            case 3:
                Debug.Log("gun3");
                break;
        }
    }
}
