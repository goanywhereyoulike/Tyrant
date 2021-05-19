using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    EnemyState enemyState = new EnemyState();

    public Transform target;
   
    private float distance = 0f;
    private float attatckdistance = 5f;

    StaticMachine behaviours = new StaticMachine();
   
    public EnemyState EnemyState { get => enemyState; set => enemyState = value; }

   


    /// <summary>

    private Animator anim;
/// </summary>


// Start is called before the first frame update
void Start()
    {
        behaviours = gameObject.GetComponent<StaticMachine>();
        behaviours.setEnemy(this);
        behaviours.AllBehaviour();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        behaviours.Update();
        distance = Vector3.Distance(transform.position, target.position);
        if (Vector3.Distance(transform.position, target.position) > enemyState.StopDistance)
        {
            enemyState.force = behaviours.ForceCalculate();
            enemyState.acceleration = enemyState.force / enemyState.Mass;
            enemyState.velocity += enemyState.acceleration;

            //animation
            anim.SetBool("isRunning", true);
        }
        else
        {
            enemyState. velocity = Vector3.zero;
/*
            if (Time.time >= enemyState.TimeBetweenAttacks)
            {

                enemyState.TimeBetweenAttacks = Time.time + enemyState.TimeBetweenAttacks;
            }
*/
            //animation
            anim.SetBool("isRunning", false);
        }
         transform.position += enemyState.velocity;


    }
    /*------------------攻击位移------------------------
    IEnumerator Attack()
    {
        //player.GetComponent<Player>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;

        Vector2 targetPosition = player.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;

            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;

            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);

            yield return null;
        }
    }
    */
    void Animation()
    {
        anim.SetBool("isRunning", true);
    
    }

}