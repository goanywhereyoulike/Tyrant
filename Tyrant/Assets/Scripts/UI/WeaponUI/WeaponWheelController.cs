using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    [SerializeField]
    private WeaponController weaponController = null;
    public Animator animator;
    private bool weaponWheelSelect = false;
    public bool WeaponWheelSelect { get=>weaponWheelSelect; set { weaponWheelSelect = value; } }
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

        if (InputManager.Instance.GetKey("WeaponWheelToggle"))
        {
            weaponWheelSelect = !weaponWheelSelect;
        }
        if (weaponWheelSelect)
        {
            WheelController(true);
        }
        else
        {
            WheelController(false);
        }
    }
    public void WheelController(bool setwheel)
    {
        animator.SetBool("OpenWeaponWheel", setwheel);
    }
}
