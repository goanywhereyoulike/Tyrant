using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trap", menuName = "Traps/Basic")]
public class TrapTemplate : ScriptableObject
{
    public string type;
    public float distanceToShoot;
    public float TrapDamage;
    public int price;
    public float CoolDownSpeed;


}
