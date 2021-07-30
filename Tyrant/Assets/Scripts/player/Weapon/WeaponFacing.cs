using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFacing : MonoBehaviour
{
    [System.Serializable]
    public struct WeaponFacingObject
    {
        public PlayerFacing playerFacing;
        public GameObject gameObject;
    }

    [SerializeField]
    private List<WeaponFacingObject> facingSprites = null;
    private Dictionary<PlayerFacing, GameObject> spriteDict = null;

    private PlayerFacing facing = PlayerFacing.Down;

    public PlayerFacing Facing
    {
        get => facing;
        set
        {
            spriteDict[facing].SetActive(false);
            facing = value;
            spriteDict[facing].SetActive(true);
        }
    }

    private void Start()
    {
        spriteDict = new Dictionary<PlayerFacing, GameObject>();

        foreach (var item in facingSprites)
        {
            spriteDict.Add(item.playerFacing, item.gameObject);
        }
    }
}
