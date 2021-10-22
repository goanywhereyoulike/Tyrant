using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trap Limit", menuName = "TrapLimit/BasicSet")]
public class TrapLimitTemplate : ScriptableObject
{
    enum TrapType { Clip, Bomb, BlackHole }
    public int RoomNumber;

}
