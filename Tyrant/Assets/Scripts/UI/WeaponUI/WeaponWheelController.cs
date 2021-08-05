using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    [SerializeField]
    private WeaponController weaponController = null;
    public Animator animator;
    private bool weaponWheelSelect = false;
    public Image selectItem;
    public Sprite noImage;
    private int weaponID;

    public int WeaponID
    {
        get => weaponID;
        set
        {
            weaponID = value;
            weaponController.ChangeWeapon(weaponID - 1);
        }
    }

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

        //switch(WeaponID)//select nothing
        //{
        //    case 0:
        //        selectItem.sprite = null ;
        //        break;
        //    case 1:
        //        Debug.Log("gun1"); //switch weapon here
        //        break;
        //    case 2:
        //        Debug.Log("gun2");
        //        break;
        //    case 3:
        //        Debug.Log("gun3");
        //        break;
        //}
    }
}
