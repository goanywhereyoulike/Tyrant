using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StaticMachine : MonoBehaviour
{
    AiBehaviours[] behaviours = null;

    Enemy mEnemy;

    public void setEnemy(Enemy enemy)
    {
        mEnemy = enemy; 
    }
    public void AllBehaviour()
    {
        behaviours = gameObject.GetComponents<AiBehaviours>();
        //foreach (var behav in gameObject.GetComponents<AiBehaviours>())
        //{
        //    behaviours.Add(behav);
        //}
       
    }

    public void Update()
    {
        for (int i = 0; i < behaviours.Length; i++)
        {
            behaviours[i].update();
        }
    }
    public Vector3 ForceCalculate()
    {
        Vector3 totalForce = Vector3.zero;
        for (int i= 0;i < behaviours.Length; i++)
        {
            if (behaviours[i].IsUse())
            {
                totalForce += behaviours[i].behaviour(mEnemy);
            }
        }
        return totalForce;
    }
}
