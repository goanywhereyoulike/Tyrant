using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StaticMachine : MonoBehaviour
{
    List<AiBehaviours> behaviours = new List<AiBehaviours>();

    Enemy mEnemy;

    public void setEnemy(Enemy enemy)
    {
        mEnemy = enemy; 
    }
    public void AllBehaviour()
    {
        foreach(var behav in gameObject.GetComponents<AiBehaviours>())
        {
            behaviours.Add(behav);
        }
       
    }

    public void Update()
    {
        for (int i = 0; i < behaviours.Count; i++)
        {
            behaviours[i].update();
        }
    }
    public Vector3 ForceCalculate()
    {
        Vector3 totalForce = Vector3.zero;
        for (int i= 0;i < behaviours.Count; i++)
        {
            if (behaviours[i].IsUse())
            {
                totalForce += behaviours[i].behaviour(mEnemy);
            }
        }
        return totalForce;
    }
}
