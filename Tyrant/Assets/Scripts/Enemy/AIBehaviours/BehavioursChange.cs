using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehavioursChange 
{
    List<AiBehaviours> behaviours = new List<AiBehaviours>();
    
    Enemy mEnemy;
    public void setEnemy(Enemy enemy)
    {
        mEnemy = enemy; 
    }

    //public void ChangeBehaviour(AiBehaviours behaviour)
    //{
    //    mBehaviour = behaviour;
    //}

    public void AddBehaviour(AiBehaviours behaviour)
    {
        behaviours.Add(behaviour);
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
