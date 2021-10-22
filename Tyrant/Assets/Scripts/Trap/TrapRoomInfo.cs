using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TrapRoomInfo
{
    public enum TrapType { Clip, Bomb, BlackHole }
    public int level;
    public TrapType trap;
}
