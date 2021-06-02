using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AiBehaviours : MonoBehaviour
{
    private bool isActive = true;

    public bool IsActive { get => isActive; set => isActive = value; }

    public virtual Vector3 behaviour(Enemy enemy) { return Vector3.zero; }

    public bool IsUse() { return IsActive;  }

    public virtual void update() { }

}
