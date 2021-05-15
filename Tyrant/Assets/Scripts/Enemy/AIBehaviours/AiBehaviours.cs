using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AiBehaviours
{
    private bool isActive = true;

    public virtual Vector3 behaviour(Enemy enemy) { return Vector3.zero; }

    public bool Continue() { return isActive = true; }

    public bool Pause() { return isActive = false; }

    public bool IsUse() { return isActive;  }

}
