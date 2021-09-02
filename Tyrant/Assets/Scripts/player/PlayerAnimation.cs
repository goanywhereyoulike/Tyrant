using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerFacing
{
    Right, Left, Up, Down, DiagUpRight, DiagUpLeft, DiagDownLeft, DiagDownRight
}

[System.Serializable]
public struct FacingSprite
{
    public PlayerFacing playerFacing;
    public Sprite sprite;
}

public class PlayerAnimation : MonoBehaviour
{
    public System.Action facingChanged = null;

    public bool IsMoving { get; set; }
    public PlayerFacing CurrentFacing
    {
        get => currentFacing;
        set
        {
            currentFacing = value;
            facingChanged?.Invoke();
        }
    }

    public SpriteRenderer spriteRenderer = null;
    public Animator animator = null;
    public WeaponController weaponController = null;

    [SerializeField]
    private List<FacingSprite> facingSprites = null;

    private Dictionary<PlayerFacing, Sprite> spriteDict = null;

    private PlayerFacing currentFacing = PlayerFacing.Down;

    private void Start()
    {
        spriteDict = new Dictionary<PlayerFacing, Sprite>();

        foreach (var item in facingSprites)
        {
            spriteDict.Add(item.playerFacing, item.sprite);
        }

        facingChanged += () => weaponController.CurrentWeapon.weaponObject.gameObject.GetComponent<WeaponFacing>().Facing = CurrentFacing;
        facingChanged += () => weaponController.CurrentWeapon.weaponObject.gameObject.GetComponent<Weapon>().Facing = CurrentFacing;
    }

    private void FixedUpdate()
    {
        Vector3 difference = InputManager.Instance.MouseWorldPosition - transform.position;

        difference.Normalize();

        spriteRenderer.flipX = difference.x < 0.0f;

        animator.enabled = IsMoving;

        if (difference.x > 0.0f && Mathf.Abs(difference.y) <= 0.1f)
            CurrentFacing = PlayerFacing.Right;
        else if (difference.x < 0.0f && Mathf.Abs(difference.y) <= 0.1f)
            CurrentFacing = PlayerFacing.Left;
        else if (Mathf.Abs(difference.x) <= 0.1f && difference.y > 0.0f)
            CurrentFacing = PlayerFacing.Up;
        else if (Mathf.Abs(difference.x) <= 0.1f && difference.y < 0.0f)
            CurrentFacing = PlayerFacing.Down;
        else if (difference.x > 0.0f && difference.y > 0.0f)
            CurrentFacing = PlayerFacing.DiagUpRight;
        else if (difference.x < 0.0f && difference.y > 0.0f)
            CurrentFacing = PlayerFacing.DiagUpLeft;
        else if (difference.x < 0.0f && difference.y < 0.0f)
            CurrentFacing = PlayerFacing.DiagDownLeft;
        else if (difference.x > 0.0f && difference.y < 0.0f)
            CurrentFacing = PlayerFacing.DiagDownRight;

        if (IsMoving)
        {
            switch(CurrentFacing)
            {
                case PlayerFacing.Left:
                    animator.SetFloat("xMouth", -1f);
                    animator.SetFloat("yMouth", 0f);
                    break;
                case PlayerFacing.Right:
                    animator.SetFloat("xMouth", 1f);
                    animator.SetFloat("yMouth", 0f);
                    break;
                case PlayerFacing.Up:
                    animator.SetFloat("xMouth", 0f);
                    animator.SetFloat("yMouth", 1f);
                    break;
                case PlayerFacing.Down:
                    animator.SetFloat("xMouth", 0f);
                    animator.SetFloat("yMouth", -1f);
                    break;
                case PlayerFacing.DiagDownLeft:
                    animator.SetFloat("xMouth", -1f);
                    animator.SetFloat("yMouth", -1f);
                    break;
                case PlayerFacing.DiagDownRight:
                    animator.SetFloat("xMouth", 1f);
                    animator.SetFloat("yMouth", -1f);
                    break;
                case PlayerFacing.DiagUpLeft:
                    animator.SetFloat("xMouth", -1f);
                    animator.SetFloat("yMouth", 1f);
                    break;
                case PlayerFacing.DiagUpRight:
                    animator.SetFloat("xMouth", 1f);
                    animator.SetFloat("yMouth", 1f);
                    break;
            }
        }
        else
        {
            spriteRenderer.sprite = spriteDict[CurrentFacing];
        }
    }
}
