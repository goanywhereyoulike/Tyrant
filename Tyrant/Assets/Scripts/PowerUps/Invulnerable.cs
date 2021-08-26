using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invulnerable : PowerUps
{
    private SpriteRenderer rend;

    [SerializeField]
    private Color colorEffect = Color.yellow;
    protected override void activeEffect()
    {
        rend = player.GetComponentInChildren<SpriteRenderer>();
        rend.material.color = colorEffect;
        player.IsInvulnerbale = true;
    }
    protected override void deactiveEffect()
    {
        player.IsInvulnerbale = false;
        rend.material.color = Color.white;
    }
}
