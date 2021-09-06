using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower Limit", menuName = "TowerLimit/BasicSet")]
public class TowerLimitTemplate : ScriptableObject
{
    public int RoomNumber;
    public List<TowerRoomInfo> towerroomInfo;

}
